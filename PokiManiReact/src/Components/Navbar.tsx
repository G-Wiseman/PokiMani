import "./Navbar.scss";
import { Link, useLocation } from "@tanstack/react-router";
import clsx from "clsx";
import { Mail, Landmark, Banknote, CircleQuestionMark, LogOut, User } from "lucide-react";

import { NavigationMenu } from "radix-ui";
import { Button } from "react-aria-components";
import { usePokiManiAuth } from "../api/PokiManiAuthProvider";

function NavbarLink({ path, textValue }: { path: string; textValue: string }) {
    const location = useLocation();

    const checkLocation = (expected: string) => {
        return location.href === expected;
    };

    const getIcon = () => {
        if (path in icons) {
            return icons[path as keyof typeof icons]; // TypeScript-safe
        }
        return <CircleQuestionMark />; // default icon
    };

    const icons = {
        "/home": <Mail />,
        "/accounts": <Landmark />,
        "/transactions": <Banknote />,
        "/profile": <User />,
    };

    return (
        <NavigationMenu.Item className="navbar__item">
            <NavigationMenu.Link asChild>
                <Link
                    to={path}
                    className={clsx("navbar__link", {
                        "navbar__link--selected": checkLocation(path),
                    })}
                >
                    {getIcon()}
                    {textValue}
                </Link>
            </NavigationMenu.Link>
        </NavigationMenu.Item>
    );
}

export default function Navbar() {
    const { logout } = usePokiManiAuth();
    return (
        <>
            <NavigationMenu.Root className="navbar">
                <NavigationMenu.List className="navbar__links">
                    <NavbarLink path="/home" textValue="Envelopes" />
                    <NavbarLink path="/accounts" textValue="Accounts" />
                    <NavbarLink path="/transactions" textValue="Transactions" />
                    <NavbarLink path="/profile" textValue="Profile" />
                </NavigationMenu.List>
                <NavigationMenu.List className="navbar__settings">
                    <Button className="navbar__logout" onClick={logout}>
                        <LogOut />
                        Logout
                    </Button>
                </NavigationMenu.List>
            </NavigationMenu.Root>
        </>
    );
}
