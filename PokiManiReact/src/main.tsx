// import "./index.scss";
import { routeTree } from "./routeTree.gen";
import { StrictMode } from "react";
import ReactDOM from "react-dom/client";
import { RouterProvider, createRouter } from "@tanstack/react-router";
import {
    PokiManiAuthProvider,
    usePokiManiAuth,
    type PokiManiAuthContextType,
} from "./api/PokiManiAuthProvider";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import "@mantine/core/styles.css";
import { MantineProvider } from "@mantine/core";

export interface RouterContext {
    auth: PokiManiAuthContextType;
}
// Create a new router instance
export const router = createRouter({
    routeTree,
    context: {} as RouterContext,
});

// Register the router instance for type safety
declare module "@tanstack/react-router" {
    interface Register {
        router: typeof router;
    }
}

function InnerApp() {
    const auth = usePokiManiAuth();
    return <RouterProvider router={router} context={{ auth }} />;
}

const queryClient = new QueryClient();
// Render the app
const rootElement = document.getElementById("root")!;
if (!rootElement.innerHTML) {
    const root = ReactDOM.createRoot(rootElement);
    root.render(
        <StrictMode>
            <QueryClientProvider client={queryClient}>
                <PokiManiAuthProvider>
                    <MantineProvider defaultColorScheme="dark">
                        <InnerApp />
                    </MantineProvider>
                </PokiManiAuthProvider>
            </QueryClientProvider>
        </StrictMode>,
    );
}
