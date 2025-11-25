import { ListBox, ListBoxItem } from "react-aria-components";
import "./Navbar.scss";
import { Link, useLocation } from "@tanstack/react-router";
import { useState } from "react";
import clsx from "clsx";

function NavbarLink({ path, textValue }: { path: string; textValue: string }) {
    const location = useLocation();

    const checkLocation = (expected: string) => {
        return location.href === expected;
    };

    return (
        <ListBoxItem className="navbar__item" textValue={textValue}>
            <Link
                to={path}
                className={clsx("navbar__link", {
                    "navbar__link--selected": checkLocation(path),
                })}
            >
                <img className="navbar__icon" src="/public/placeholder.svg" />

                {textValue}
            </Link>
        </ListBoxItem>
    );
}

export default function Navbar() {
    return (
        <>
            <ListBox className="navbar" aria-label="Links">
                <NavbarLink path="/home" textValue="Home" />
                <NavbarLink path="/accounts" textValue="Accounts" />
                <NavbarLink path="/transactions" textValue="Transactions" />
            </ListBox>
        </>
    );
}
