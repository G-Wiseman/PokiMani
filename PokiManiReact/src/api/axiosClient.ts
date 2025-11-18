import axios from "axios";

export const axiosClient = axios.create({
    baseURL: "https://localhost:7017/api",
    withCredentials: true,
});

export const orvalClient = async <T>({
    url,
    method,
    data,
    headers,
    signal,
}: {
    url: string;
    method: string;
    data?: unknown;
    headers?: Record<string, string>;
    signal?: AbortSignal;
}): Promise<T> => {
    const response = await axiosClient.request<T>({
        url,
        method,
        data,
        headers,
        signal,
    });
    return response.data;
};
