import { useState } from "react";
import { useDeleteApiEnvelopesId, useGetApiEnvelopesId } from "../api/PokiManiApi";
import "./EnvelopeLine.scss";
import { clsx } from "clsx";
import { useQueryClient } from "@tanstack/react-query";
import { NumberInput } from "@mantine/core";
import { DollarSign } from "lucide-react";

export default function EnvelopeLine({ envId }: { envId: string }) {
    const [balanceUpdate, setBalanceUpdate] = useState(0);
    const queryClient = useQueryClient();
    const currencyIcon = <DollarSign />;

    const { mutateAsync: deleteAsync } = useDeleteApiEnvelopesId();

    const { data, isLoading } = useGetApiEnvelopesId(envId, {
        query: { queryKey: ["envelopes", envId], staleTime: 1000 * 60 * 5 },
    });
    if (isLoading) {
        return <div>loading</div>;
    }

    return (
        <>
            <div className="envelopeLine__outer">
                <div className="envelopeLine__name">{data!.name!}</div>
                <div className="envelopeLine__balance">
                    <div
                        className={clsx(
                            "envelopeLine__balance_current",
                            balanceUpdate !== 0 && "envelopeLine__balance_current--updating",
                        )}
                    >
                        {data!.balance!}
                    </div>
                    <div
                        className={clsx(
                            "envelopeLine__balance_updated",
                            balanceUpdate === 0 && "envelopeLine__balance_updated--invisible",
                            balanceUpdate !== 0 && "envelopeLine__balance_updated--visible",
                        )}
                    >
                        {data!.balance! + balanceUpdate}
                    </div>
                </div>

                <NumberInput
                    onChange={value => {
                        const parsed = Number(value);
                        if (!Number.isNaN(parsed) && Number.isFinite(parsed)) {
                            setBalanceUpdate(parsed);
                        }
                    }}
                    placeholder="Update Balance"
                    leftSection={currencyIcon}
                    hideControls
                    decimalScale={2}
                ></NumberInput>
                <button
                    className="envelopeLine__delete"
                    onClick={async () => {
                        await deleteAsync(
                            { id: envId },
                            {
                                onSuccess: () => {
                                    queryClient.removeQueries({ queryKey: ["envelopes", envId] });
                                    queryClient.invalidateQueries({ queryKey: ["envelopes"] });
                                },
                            },
                        );
                    }}
                >
                    Delete
                </button>
            </div>
        </>
    );
}
