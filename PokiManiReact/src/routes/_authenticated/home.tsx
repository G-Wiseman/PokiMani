import { useQueryClient } from "@tanstack/react-query";
import { createFileRoute } from "@tanstack/react-router";
import { usePokiManiAuth } from "../../api/PokiManiAuthProvider";
import { useGetApiEnvelopes, usePostApiEnvelopes } from "../../api/PokiManiApi";
import EnvelopeLine from "../../Components/EnvelopeLine";

export const Route = createFileRoute("/_authenticated/home")({
    component: Home,
    beforeLoad: () => {},
});

export default function Home() {
    const queryClient = useQueryClient();
    const { isAuthenticated, logout } = usePokiManiAuth();
    const { mutate: newEnvelope } = usePostApiEnvelopes();
    const { data: envs } = useGetApiEnvelopes({
        query: {
            queryKey: ["envelopes"],
            select: envelopes => {
                envelopes.forEach(e => {
                    queryClient.setQueryData(["envelopes", e.id], e);
                });
                return envelopes;
            },
            staleTime: 1000,
        },
    });
    // const { data: accts } = useGetApiAccounts({ query: { queryKey: ["accounts"] } });
    // const { data: acctTs } = useGetApiAccountTransactions({
    //     query: { queryKey: ["account-transactions"] },
    // });
    // const { data: envTs } = useGetApiEnvelopeTransactions({
    //     query: { queryKey: ["envelope-transactions"] },
    // });
    const logit = () => {
        console.log(queryClient.getQueryCache().getAll());
    };
    const newEnv = () => {
        newEnvelope(
            {
                data: {
                    name: "Random Envelope Name",
                    balance: Math.floor(Math.random() * 500),
                },
            },
            {
                onSuccess: () => {
                    queryClient.invalidateQueries({ queryKey: ["envelopes"] });
                },
            },
        );
    };

    const envsList = envs ? (
        envs.map(env => <EnvelopeLine envId={env.id!} key={env.id} />)
    ) : (
        <div></div>
    );

    return (
        <div>
            <button onClick={logout}>Log Out</button>
            <button onClick={newEnv}> Make a random new Envelope! </button>
            <button onClick={logit}> Show me the cache </button>
            {envsList}
            <div>{isAuthenticated.toString()}</div>
        </div>
    );
}
