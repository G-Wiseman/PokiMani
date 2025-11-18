import { defineConfig } from "orval";

export default defineConfig({
    PokiManiApi: {
        input: "./src/api/OpenApiSpec.json",
        output: {
            baseUrl: "https://localhost:7017",
            target: "./src/api/PokiManiApi.ts",
            mode: "single",
            client: "react-query",

            override: {
                mutator: {
                    path: "./src/api/axiosClient.ts",
                    name: "orvalClient",
                },
                query: {
                    useQuery: true,
                },
            },
        },
    },
});
