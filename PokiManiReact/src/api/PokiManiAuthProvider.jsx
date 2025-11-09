import { createContext, useContext, useLayoutEffect, useRef, useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { pokiManiApiAxios } from "./apiClient";
import { useQueryClient } from "@tanstack/react-query";

const PokiManiAuthContext = createContext();

export const PokiManiAuthProvider = ({ children }) => {
    const queryClient = useQueryClient();
    const navigate = useNavigate();
    const jwt = useRef(); // in-memory only
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    useLayoutEffect(() => {
        pokiManiApiAxios.interceptors.request.use(config => {
            if (jwt.current) {
                config.headers["Authorization"] = `Bearer ${jwt.current}`;
            }
            return config;
        });

        pokiManiApiAxios.interceptors.response.use(
            res => res, // normal response
            async err => {
                const originalRequest = err.config;

                if (err.response?.status === 401 && !originalRequest._retry) {
                    originalRequest._retry = true;
                    try {
                        // Attempt refresh
                        if (originalRequest.url === "/Auth/refresh") {
                            // DO NOT RECURSE INFINTELY HERE. STOP IF EVER RETRYING A REFRESH...
                            throw new error(err);
                        }

                        await refresh();
                        // Retry the original request
                        originalRequest.headers["Authorization"] = `Bearer ${jwt.current}`;
                        return await pokiManiApiAxios.request(originalRequest);
                    } catch {
                        // Refresh failed, log out

                        logout();
                        return Promise.reject(err);
                    }
                }
                console.log("why is it here?", originalRequest.url);
                return Promise.reject(err);
            },
        );
    }, []);

    // Login function
    const login = async (username, password) => {
        try {
            const response = await pokiManiApiAxios.post("/Auth/token", {
                userName: username,
                password: password,
            });

            if (response.status === 200 && response.data.accessToken) {
                jwt.current = response.data.accessToken;
                setIsAuthenticated(true);
                return { success: true };
            } else {
                return {
                    success: false,
                    message: "Invalid response from server",
                };
            }
        } catch (err) {
            return {
                success: false,
                message: err.response?.data?.message || err.message || "Login failed",
            };
        }
    };

    const refresh = async () => {
        try {
            const response = await pokiManiApiAxios.post("/Auth/refresh");
            console.log("refresh response", response);
            if (response.status === 200 && response.data.accessToken) {
                jwt.current = response.data.accessToken;
                setIsAuthenticated(true);
                return { success: true, token: response.data.accessToken };
            } else {
                jwt.current = null;
                setIsAuthenticated(false);
                return { success: false, message: "Refresh failed" };
            }
        } catch (err) {
            jwt.current = null;
            setIsAuthenticated(false);
            return {
                success: false,
                message: err.response?.data?.message || err.message || "Refresh failed",
            };
        }
    };
    // Logout function
    const logout = () => {
        pokiManiApiAxios.post("/Auth/logout");
        jwt.current = null;
        setIsAuthenticated(false);
        queryClient.removeQueries();
        navigate("/login");
    };

    return (
        <PokiManiAuthContext.Provider
            value={{
                isAuthenticated,
                setIsAuthenticated,
                login,
                logout,
                refresh,
            }}
        >
            {children}
        </PokiManiAuthContext.Provider>
    );
};

export const usePokiManiApi = () => useContext(PokiManiAuthContext);
