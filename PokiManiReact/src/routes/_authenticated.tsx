import { createFileRoute, redirect, Outlet, useNavigate } from "@tanstack/react-router";
import { useEffect } from "react";
import { usePokiManiAuth } from "../api/PokiManiAuthProvider";
export const Route = createFileRoute("/_authenticated")({
    component: AuthenticatedLayout,
    beforeLoad: async ({ context, location }) => {
        if (!context.auth.isAuthenticated) {
            throw redirect({
                to: "/login",
                search: {
                    redirect: location.href,
                },
            });
        }
    },
});
export default function AuthenticatedLayout() {
    const { isAuthenticated } = usePokiManiAuth();
    const navigate = useNavigate();
    useEffect(() => {
        if (!isAuthenticated) {
            navigate({
                to: "/login",
            });
        }
    }, [isAuthenticated, navigate]);

    return <Outlet />;
}
