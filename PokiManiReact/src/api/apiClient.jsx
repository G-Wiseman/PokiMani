import axios from "axios";

export const pokiManiApiAxios = axios.create({
    baseURL: "https://localhost:7017/api",
    withCredentials: true,
});
