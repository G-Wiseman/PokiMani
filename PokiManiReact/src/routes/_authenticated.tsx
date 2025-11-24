import { createFileRoute, redirect, Outlet, useNavigate } from "@tanstack/react-router";
import { useEffect, useState } from "react";
import { usePokiManiAuth } from "../api/PokiManiAuthProvider";
import "./_authenticated.scss";
import Banner from "../Components/Banner";
import Navbar from "../Components/Navbar";
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
    const [sidebarOpen, setSidebarOpen] = useState(false);

    return (
        <div className="layout">
            <aside className="layout__navbar">
                <Navbar />
            </aside>
            <header className="layout__header">
                <Banner />
            </header>
            <div className="layout__content">
                <Outlet />
            </div>
        </div>
    );
}
