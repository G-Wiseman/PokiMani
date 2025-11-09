import React from "react";
import { Link } from "react-router-dom";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { pokiManiApiAxios as api } from "../../api/apiClient";
import { usePokiManiApi } from "../../api/PokiManiAuthProvider";

export default function Login() {
    const { isAuthenticated, setIsAuthenticated, login, logout, refresh } = usePokiManiApi();
    const navigate = useNavigate();
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleSubmit = async e => {
        e.preventDefault();
        setError("");

        try {
            const res = await login(username, password);
            console.log(res);
            if (res.success == true) {
                navigate("/home");
            }
        } catch (err) {
            setError("Login failed");
            console.error(err);
        }
    };
    return (
        <div>
            <h1>Login</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Username:</label>
                    <input
                        type="text"
                        value={username}
                        onChange={e => setUsername(e.target.value)}
                        required
                    />
                </div>

                <div>
                    <label>Password:</label>
                    <input
                        type="password"
                        value={password}
                        onChange={e => setPassword(e.target.value)}
                        required
                    />
                </div>

                <button type="submit">Login</button>
            </form>

            {error && <p style={{ color: "red" }}>{error}</p>}

            <p>
                Go to <Link to="/">HomePage</Link>
            </p>
        </div>
    );
}
