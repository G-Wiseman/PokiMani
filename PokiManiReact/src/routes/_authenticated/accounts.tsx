import { createFileRoute } from "@tanstack/react-router";
import { useGetApiAccounts, usePostApiAccounts } from "../../api/PokiManiApi";
import { useQueryClient } from "@tanstack/react-query";

export const Route = createFileRoute("/_authenticated/accounts")({
    component: AccountsPage,
});

function AccountsPage() {
    const queryClient = useQueryClient();
    const { mutate: newAccount } = usePostApiAccounts();
    const { data: accounts } = useGetApiAccounts({
        query: {
            queryKey: ["accounts"],
            staleTime: 1000 * 60 * 5,
            select: accounts => {
                accounts.forEach(acc => {
                    queryClient.setQueryData(["account", acc.id], acc);
                });
            },
        },
    });

    return <div>Hello "/_authenticated/accounts"!</div>;
}
