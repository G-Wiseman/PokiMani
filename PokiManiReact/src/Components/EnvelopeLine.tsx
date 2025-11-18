import { useGetApiEnvelopesId } from "../api/PokiManiApi";

export default function EnvelopeLine({ envId }: { envId: string }) {
    const { data, isLoading } = useGetApiEnvelopesId(envId, {
        query: { queryKey: ["envelopes", envId] },
    });
    if (isLoading) {
        return <div>loading</div>;
    }

    return (
        <>
            <div className="envelopeLine__outer">
                <div className="envelopeLine__name">{data!.name!}</div>
                <div className="envelopeLine__balance">{data!.balance!}</div>
                <div className="envelopeLine__"></div>
            </div>
        </>
    );
}
