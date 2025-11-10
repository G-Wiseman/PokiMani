import { createContext, useContext, useLayoutEffect, useRef, useState } from "react";
import { useNavigate } from "react-router";
import { useQueryClient } from "@tanstack/react-query";
import { pokiManiApiAxios } from "./axiosClient";

type authResult = { success: boolean; message: string };

interface PokiManiAuthContextType {
    isAuthenticated: boolean;
    setIsAuthenticated: React.Dispatch<React.SetStateAction<boolean>>;
    login: (username: string, password: string) => Promise<authResult>;
    logout: () => void;
    refresh: () => Promise<authResult>;
}

interface PokiManiAuthProviderProps {
    children: React.ReactNode;
}

const PokiManiAuthContext = createContext<PokiManiAuthContextType>();

export const PokiManiAuthProvider = ({ children }: PokiManiAuthProviderProps) => {
    const queryClient = useQueryClient();
    const navigate = useNavigate();
    const jwt = useRef(null); // in-memory only
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
                            throw new Error(err);
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
    const login = async (username: string, password: string) => {
        try {
            const response = await pokiManiApiAxios.post("/Auth/token", {
                userName: username,
                password: password,
            });

            if (response.status === 200 && response.data.accessToken) {
                jwt.current = response.data.accessToken;
                setIsAuthenticated(true);
                return { success: true, message: "" };
            } else {
                return {
                    success: false,
                    message: "Invalid response from server",
                };
            }
        } catch {
            return {
                success: false,
                message: "Login failed",
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
                return { success: true, message: "" };
            } else {
                jwt.current = null;
                setIsAuthenticated(false);
                return { success: false, message: "Refresh failed" };
            }
        } catch {
            jwt.current = null;
            setIsAuthenticated(false);
            return { success: false, message: "Refresh failed" };
        }
    };
    // Logout function
    const logout = async () => {
        try {
            await pokiManiApiAxios.post("/Auth/logout"); // await the promise
        } catch (error) {
            console.error("Logout failed:", error);
        } finally {
            if (jwt) jwt.current = null;
            setIsAuthenticated(false);
            queryClient.removeQueries();
            navigate("/login");
        }
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
