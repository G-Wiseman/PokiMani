import { createFileRoute, useNavigate, useRouter, useSearch } from "@tanstack/react-router";
import { usePokiManiAuth } from "../api/PokiManiAuthProvider";
import { useEffect, useReducer, useState } from "react";
import Home from "./_authenticated/home";

export const Route = createFileRoute("/login")({
    component: Login,
});

export default function Login() {
    const { isAuthenticated, login, register } = usePokiManiAuth();
    const [loginUsername, setLoginUsername] = useState("");
    const [loginPassword, setLoginPassword] = useState("");
    const [registerUsername, setRegisterUsername] = useState("");
    const [registerPassword, setRegisterPassword] = useState("");
    const [registerEmail, setRegisterEmail] = useState("");
    const search = useSearch({ from: "/login" });
    const navigate = useNavigate();
    useEffect(() => {
        if (isAuthenticated) {
            console.log("search", search);
            // Only navigate to redirect if it exists, otherwise fallback to /home
            navigate({ to: "/home" });
        }
    }, [isAuthenticated]);

    const handleLoginSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        await login(loginUsername, loginPassword);
    };

    const handleRegisterSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        await register(registerUsername, registerEmail, registerPassword);
    };

    return (
        <div>
            <h1>{isAuthenticated ? "Acutally Already Authed" : "Not Logged In"}</h1>
            <form onSubmit={handleLoginSubmit}>
                <div>
                    <label>Username:</label>
                    <input
                        type="text"
                        value={loginUsername}
                        onChange={e => setLoginUsername(e.target.value)}
                        required
                    />
                </div>

                <div>
                    <label>Password:</label>
                    <input
                        type="password"
                        value={loginPassword}
                        onChange={e => setLoginPassword(e.target.value)}
                        required
                    />
                </div>
                <input type="submit" style={{ display: "none" }} />
                <button type="submit">Log in</button>
            </form>

            <h1>Register</h1>
            <form onSubmit={handleRegisterSubmit}>
                <div>
                    <label>Email:</label>
                    <input
                        type="text"
                        value={registerUsername}
                        onChange={e => setRegisterUsername(e.target.value)}
                        required
                    />
                </div>

                <div>
                    <label>Username:</label>
                    <input
                        type="text"
                        value={registerEmail}
                        onChange={e => setRegisterEmail(e.target.value)}
                        required
                    />
                </div>

                <div>
                    <label>Password:</label>
                    <input
                        type="password"
                        value={registerPassword}
                        onChange={e => setRegisterPassword(e.target.value)}
                        required
                    />
                </div>
                <button type="submit">Register Account</button>
            </form>
        </div>
    );
}
