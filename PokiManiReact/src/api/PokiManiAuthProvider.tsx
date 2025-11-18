import {
    createContext,
    useCallback,
    useContext,
    useEffect,
    useLayoutEffect,
    useRef,
    useState,
} from "react";
import { axiosClient } from "./axiosClient";
import {
    usePostApiAuthLogin,
    usePostApiAuthLogout,
    usePostApiAuthRefresh,
    usePostApiAuthRegister,
} from "./PokiManiApi";
import { useQueryClient } from "@tanstack/react-query";

export interface PokiManiAuthContextType {
    isAuthenticated: boolean;
    setIsAuthenticated: React.Dispatch<React.SetStateAction<boolean>>;
    isLoading: boolean;
    login: (username: string, password: string) => void;
    logout: () => void;
    refresh: () => void;
    register: (name: string, email: string, password: string) => void;
}

const PokiManiAuthContext = createContext<PokiManiAuthContextType>(undefined!);
export const PokiManiAuthProvider = ({ children }: { children: React.ReactNode }) => {
    const queryClient = useQueryClient();
    const { mutate: apiLogin } = usePostApiAuthLogin();
    const { mutate: apiRefresh } = usePostApiAuthRefresh();
    const { mutate: apiLogout } = usePostApiAuthLogout();
    const { mutate: apiRegister } = usePostApiAuthRegister();
    // TODO: Add registration handler to this?
    const jwt = useRef<string>(null); // in-memory only
    const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
    const refreshPromsise = useRef<Promise<void> | void>(null);
    const isMounted = useRef<boolean>(false);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    // Login function
    const login = async (username: string, password: string) => {
        await apiLogin(
            { data: { userName: username, password: password } },
            {
                onSuccess: token => {
                    jwt.current = token.accessToken;
                    setIsAuthenticated(true);
                },
                onError: () => {
                    jwt.current = null;
                    setIsAuthenticated(false);
                },
            },
        );
    };

    const refresh = useCallback(async () => {
        await apiRefresh(undefined, {
            onSuccess: token => {
                jwt.current = token.accessToken;
                setIsAuthenticated(true);
            },
            onError: err => {
                jwt.current = null;
                console.log("it did fail");
                setIsAuthenticated(false);
                throw err;
            },
        });
    }, [apiRefresh]);

    // Logout function
    const logout = useCallback(async () => {
        await apiLogout(undefined, {
            onSuccess: () => {
                queryClient.clear();
                jwt.current = null;
                setIsAuthenticated(false);
            },
            onError: err => {
                console.log(err);
                // TODO: handle the case where a user ISN'T logged out properly.
            },
        });
    }, [apiLogout, queryClient]);

    const register = async (name: string, email: string, password: string) => {
        apiRegister(
            {
                data: {
                    name: name,
                    email: email,
                    password: password,
                },
            },
            {
                onSuccess: token => {
                    jwt.current = token.accessToken;
                    setIsAuthenticated(true);
                },
            },
        );
    };
    // Runs ONCE on first page load
    useEffect(() => {
        const init = async () => {
            if (!isMounted.current) {
                try {
                    if (!refreshPromsise.current) {
                        refreshPromsise.current = refresh()
                            .then(() => {
                                console.log("hei");
                                setIsAuthenticated(true);
                            })
                            .catch(err => {
                                console.log("failed to refresh");
                                setIsAuthenticated(false);
                                throw err;
                            })
                            .finally(() => {
                                refreshPromsise.current = null;
                            });
                    }
                    await refreshPromsise;
                    // setIsAuthenticated(true);
                } catch {
                    // setIsAuthenticated(false);
                }
            }
            setIsLoading(false);
            isMounted.current = true;
        };
        init();
    }, [refresh]); // empty deps = exactly once

    // change interceptors to new jwt
    useLayoutEffect(() => {
        const reqInterceptor = axiosClient.interceptors.request.use(async config => {
            if (refreshPromsise.current) {
                // Just pause while refreshing to get newest token.
                await refreshPromsise.current;
            }

            if (jwt.current) {
                config.headers["Authorization"] = `Bearer ${jwt.current}`;
            }
            return config;
        });

        const respInterceptor = axiosClient.interceptors.response.use(
            res => res, // normal response
            async err => {
                const originalRequest = err.config;
                if (originalRequest.url?.endsWith("api/auth/refresh")) {
                    // DO NOT RECURSE INFINTELY HERE. STOP IF EVER RETRYING A REFRESH...
                    throw err;
                }

                if (err.response?.status === 401 && !originalRequest._retry) {
                    originalRequest._retry = true;
                    try {
                        // Attempt refresh

                        if (!refreshPromsise.current) {
                            refreshPromsise.current = refresh()
                                .then(() => {
                                    refreshPromsise.current = null;
                                    setIsAuthenticated(true);
                                })
                                .catch(err => {
                                    setIsAuthenticated(false);
                                    throw err;
                                });
                        }
                        await refreshPromsise;
                        // Retry the original request
                        originalRequest.headers["Authorization"] = `Bearer ${jwt.current}`;
                        return await axiosClient.request(originalRequest);
                    } catch {
                        // Refresh failed, log out

                        await logout();
                        return Promise.reject(err);
                    }
                }
                return Promise.reject(err);
            },
        );
        return () => {
            axiosClient.interceptors.request.eject(reqInterceptor);
            axiosClient.interceptors.response.eject(respInterceptor);
        };
    }, [refresh, logout]);

    return (
        <PokiManiAuthContext.Provider
            value={{
                isAuthenticated,
                setIsAuthenticated,
                isLoading,
                login,
                logout,
                refresh,
                register,
            }}
        >
            {children}
        </PokiManiAuthContext.Provider>
    );
};

export const usePokiManiAuth = () => useContext(PokiManiAuthContext);
