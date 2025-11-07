import {QueryClient, QueryClientProvider} from "@tanstack/react-query"
import Layout from "../Layout/Layout";
import React from 'react'

const queryClient = new QueryClient();

export default function App() {

  return (
    <>
    <QueryClientProvider client={queryClient}>
        <Layout/>
    </QueryClientProvider>
    </>
  )
}
