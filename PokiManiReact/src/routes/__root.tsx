import { createRootRouteWithContext, Link, Outlet } from "@tanstack/react-router";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";
import type { RouterContext } from "../main";

const RootLayout = () => (
    <>
        <div className="p-2 flex gap-2">
            <Link to="/login" className="[&.active]:font-bold">
                Login
            </Link>
            <Link to="/home" className="[&.active]:font-bold">
                Home
            </Link>
        </div>
        <hr />
        <Outlet />
        <TanStackRouterDevtools />
    </>
);

export const Route = createRootRouteWithContext<RouterContext>()({ component: RootLayout });
