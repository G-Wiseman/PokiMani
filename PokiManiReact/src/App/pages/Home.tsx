import { useQuery, useQueryClient } from "@tanstack/react-query";

import { usePokiManiApi } from "../../api/PokiManiAuthProvider";
import { pokiManiApiAxios as api } from "../../api/axiosClient";

export default function Home() {
    const queryClient = useQueryClient();
    const { logout } = usePokiManiApi();
    const fetchEnvelopes = async () => {
        const response = await api.get("/envelopes");
        return response.data;
    };
    const { data: envelopes } = useQuery({
        queryKey: ["envelopes"],
        queryFn: fetchEnvelopes,
    });

    const makeEnvelope = async () => {
        const values = {
            balance: 0,
            color: "string",
            isParent: true,
            name: "string",
            parentId: null,
            dateCreated: Date.UTC,
            dateDestroyed: null,
        };
        try {
            await api.post("/envelopes", values);
            queryClient.invalidateQueries({ queryKey: ["envelopes"] });
        } catch {}
    };

    return (
        <div>
            <button onClick={logout}>Log Out</button>
            <button onClick={makeEnvelope}> Add an envelope </button>
            <div></div>
        </div>
    );
}
