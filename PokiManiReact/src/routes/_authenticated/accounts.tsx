import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/_authenticated/accounts")({
    component: RouteComponent,
});

function RouteComponent() {
    return <div>Hello "/_authenticated/accounts"!</div>;
}
