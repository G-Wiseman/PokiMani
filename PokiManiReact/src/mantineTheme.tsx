import { createTheme } from "@mantine/core";

export default function mantineTheme() {
    const theme = createTheme({
        defaultRadius: 5,
        colors: {
            dark: [
                "#F8F8F2",
                "#afb0c7",
                "#999aaf",
                "#848498",
                "#323246",
                "#2b2c3d",
                "#252534",
                "#1f1f2c",
                "#191923",
                "#13131b",
            ],
            pink: [
                "#ffe8fa",
                "#ffceec",
                "#ff9bd5",
                "#ff79c6",
                "#fe37a9",
                "#fe1c9d",
                "#ff0996",
                "#e40083",
                "#cc0074",
                "#b30065",
            ],
        },
        primaryColor: "pink",
    });

    return theme;
}
