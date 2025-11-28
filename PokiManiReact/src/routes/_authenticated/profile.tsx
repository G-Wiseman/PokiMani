import { createFileRoute } from "@tanstack/react-router";
import { usePokiManiAuth } from "../../api/PokiManiAuthProvider";
import { Button } from "@mantine/core";
import { LogOut } from "lucide-react";

export const Route = createFileRoute("/_authenticated/profile")({
    component: RouteComponent,
});

function RouteComponent() {
    const { logout } = usePokiManiAuth();
    return (
        <Button leftSection={<LogOut />} variant="filled" onClick={logout}>
            Logout
        </Button>
    );
}
