import { createFileRoute, redirect, Outlet, useNavigate } from "@tanstack/react-router";
import { useEffect, useState } from "react";
import { usePokiManiAuth } from "../api/PokiManiAuthProvider";
import { Header } from "react-aria-components";
import "./_authenticated.scss";
import Banner from "../Components/Banner";
import Navbar from "../Components/Navbar";
import { AppShell, Burger, useMantineTheme } from "@mantine/core";
import { useDisclosure, useMediaQuery } from "@mantine/hooks";
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

    const theme = useMantineTheme();
    const isMobile = useMediaQuery(`(max-width: ${theme.breakpoints.sm})`);
    console.log(isMobile);
    return (
        <AppShell
            layout="alt"
            padding="md"
            navbar={{ width: 250, breakpoint: "sm" }}
            header={{ height: 80 }}
            footer={{ height: 80 }}
        >
            {/* Conditionally render Navbar only on desktop */}
            {!isMobile && (
                <AppShell.Navbar>
                    <Navbar />
                </AppShell.Navbar>
            )}

            <AppShell.Header style={{ display: "flex" }}>
                <Banner />
            </AppShell.Header>

            <AppShell.Main>
                <Outlet />
            </AppShell.Main>

            {/* Footer only on mobile */}
            {isMobile && (
                <AppShell.Footer style={{ display: "flex" }}>
                    <Navbar />
                </AppShell.Footer>
            )}
        </AppShell>
    );
}
