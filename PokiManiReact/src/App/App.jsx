import React from "react";
import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import Home from "./pages/Home.jsx";
import Login from "./pages/Login.jsx";
import Redirect from "./pages/Redirect.jsx";
import { PokiManiAuthProvider } from "../api/PokiManiAuthProvider";

const queryClient = new QueryClient();

export default function App() {
    return (
        <QueryClientProvider client={queryClient}>
            <Router>
                <PokiManiAuthProvider>
                    <Routes>
                        <Route path="/" element={<Redirect />}></Route>
                        <Route path="/home" element={<Home />} />
                        <Route path="/login" element={<Login />} />
                    </Routes>
                </PokiManiAuthProvider>
            </Router>
        </QueryClientProvider>
    );
}
