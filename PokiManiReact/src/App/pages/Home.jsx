import { useQuery, useQueryClient } from "@tanstack/react-query";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { pokiManiApiAxios as api } from "../../api/apiClient";
import { usePokiManiApi } from "../../api/PokiManiAuthProvider";

export default function Home() {
    const queryClient = useQueryClient();
    const { isAuthenticated, setIsAuthenticated, logout } = usePokiManiApi();
    const navigate = useNavigate();
    const data = 5;
    const fetchEnvelopes = async () => {
        const response = await api.get("/envelopes");
        return response.data;
    };
    const {
        data: envelopes,
        error,
        isLoading,
    } = useQuery({
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
            dateCreated: Date.UTC(),
            dateDestroyed: null,
        };
        try {
            await api.post("/envelopes", values);
            queryClient.invalidateQueries(["envelopes"]);
        } catch {}
    };

    return (
        <div>
            <button onClick={logout}>Log Out</button>
            <button onClick={makeEnvelope}> Add an envelope </button>

            <div>
                {envelopes?.map(env => (
                    <div key={env.id}>
                        {env.name} - {env.balance}
                    </div>
                ))}
            </div>
        </div>
    );
}
