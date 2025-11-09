import React, { useState } from "react";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import "./Banner.scss";

export default function Banner() {
    const [looseCash, setLooseCash] = useState(0);
    const [looseCashIsNegative, setLooseCashIsNegative] = useState(false);

    const queryClient = useQueryClient();

    const { data, isLoading } = useQuery({
        queryFn: () => fetch("https://jsonplaceholder.typicode.com/todos/1"),
        queryKey: ["Banner"],
    });

    const { mutateAsync: bannerAsyncMutate } = useMutation({
        mutationFn: () =>
            fetch("https://jsonplaceholder.typicode.com/todos/1", {
                method: "POST",
            }),
        onSuccess: () => {
            queryClient.invalidateQueries(["Banner"]);
        },
    });

    return (
        <>
            <section className="banner">
                <div
                    className="banner__component"
                    onClick={() => {
                        setLooseCash(looseCash + 1);
                    }}
                >
                    <div className="banner__text">"Loose Cash"</div>
                    <div className="banner__text">{looseCash}</div>
                </div>
                <div className="banner__component">October</div>
            </section>
        </>
    );
}
