import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Redirect from "./pages/Redirect";
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
