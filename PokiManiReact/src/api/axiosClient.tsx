import axios, { type AxiosInstance } from "axios";

export const pokiManiApiAxios: AxiosInstance = axios.create({
  baseURL: "https://localhost:7017/api",
  withCredentials: true,
});
