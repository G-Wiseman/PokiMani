import {
    createContext,
    useCallback,
    useContext,
    useEffect,
    useLayoutEffect,
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
    login: (username: string, password: string) => void;
    logout: () => void;
    refresh: () => void;
    register: (name: string, email: string, password: string) => void;
}
let refreshPromsise: Promise<void> | null = null;
let jwt: string | null = null; // in-memory only

const getJwt = () => {
    return jwt;
};

const setJwt = (newJwt: string | null) => {
    jwt = newJwt;
};

const PokiManiAuthContext = createContext<PokiManiAuthContextType>(undefined!);
export const PokiManiAuthProvider = ({ children }: { children: React.ReactNode }) => {
    const queryClient = useQueryClient();
    const { mutateAsync: apiLogin } = usePostApiAuthLogin();
    const { mutateAsync: apiRefresh } = usePostApiAuthRefresh();
    const { mutateAsync: apiLogout } = usePostApiAuthLogout();
    const { mutateAsync: apiRegister } = usePostApiAuthRegister();
    // TODO: Add registration handler to this?
    const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);

    // Login function
    const login = useCallback(
        async (username: string, password: string) => {
            await apiLogin(
                { data: { userName: username, password: password } },
                {
                    onSuccess: token => {
                        setJwt(token.accessToken);
                        setIsAuthenticated(true);
                    },
                    onError: () => {
                        setJwt(null);
                        setIsAuthenticated(false);
                    },
                },
            );
        },
        [apiLogin],
    );

    const refresh = useCallback(async () => {
        await apiRefresh(undefined, {
            onSuccess: token => {
                setJwt(token.accessToken);
                setIsAuthenticated(true);
            },
            onError: err => {
                setJwt(null);
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
                setJwt(null);
                setIsAuthenticated(false);
            },
            onError: err => {
                console.log(err);
                // TODO: handle the case where a user ISN'T logged out properly.
            },
        });
    }, [apiLogout, queryClient]);

    const register = useCallback(
        async (name: string, email: string, password: string) => {
            await apiRegister(
                {
                    data: {
                        name: name,
                        email: email,
                        password: password,
                    },
                },
                {
                    onSuccess: token => {
                        setJwt(token.accessToken);
                        setIsAuthenticated(true);
                    },
                },
            );
        },
        [apiRegister],
    );
    // Runs ONCE on first page load
    useEffect(() => {
        const init = async () => {
            try {
                if (!refreshPromsise) {
                    refreshPromsise = refresh()
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
                            refreshPromsise = null;
                        });
                }
                await refreshPromsise;
            } catch {}
        };
        init();
    }, [refresh]); // empty deps = exactly once

    // change interceptors to new jwt
    useLayoutEffect(() => {
        const reqInterceptor = axiosClient.interceptors.request.use(async config => {
            config.headers["Authorization"] = `Bearer ${getJwt()}`;
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

                        if (!refreshPromsise) {
                            refreshPromsise = refresh()
                                .then(() => {
                                    refreshPromsise = null;
                                    setIsAuthenticated(true);
                                })
                                .catch(err => {
                                    setIsAuthenticated(false);
                                    throw err;
                                });
                        }
                        await refreshPromsise;
                        // Retry the original request
                        originalRequest.headers["Authorization"] = `Bearer ${getJwt()}`;
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
