import classes from "./Navbar.module.css";
import { Link } from "@tanstack/react-router";
import { Mail, Landmark, Banknote, CircleQuestionMark, User } from "lucide-react";
import { Flex, NavLink, useMantineTheme } from "@mantine/core";

function NavbarLink({ path, textValue }: { path: string; textValue: string }) {
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
        <NavLink
            classNames={{ label: classes.navbar__linklabel, root: classes.navbar__linkroot }}
            label={textValue}
            leftSection={getIcon()}
            component={Link}
            to={path}
        ></NavLink>
    );
}

export default function Navbar() {
    const theme = useMantineTheme();
    return (
        <Flex direction={{ base: "row", sm: "column" }} p={10} rowGap={4}>
            <NavbarLink path="/home" textValue="Envelopes" />
            <NavbarLink path="/accounts" textValue="Accounts" />
            <NavbarLink path="/transactions" textValue="Transactions" />
            <NavbarLink path="/profile" textValue="Profile" />
        </Flex>
    );
}
