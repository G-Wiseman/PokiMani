import { createRootRouteWithContext, Link, Outlet } from "@tanstack/react-router";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";
import type { RouterContext } from "../main";

const RootLayout = () => <Outlet />;

export const Route = createRootRouteWithContext<RouterContext>()({ component: RootLayout });
