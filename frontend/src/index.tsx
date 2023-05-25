import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import "@fontsource/roboto/300.css";
import "@fontsource/roboto/400.css";
import "@fontsource/roboto/500.css";
import "@fontsource/roboto/700.css";
import { BrowserRouter } from "react-router-dom";
import { ModalBoxProvider } from "components/modalBox/providers/modalBox";
import { QueryClientProvider } from "react-query";
import { queryClient } from "api";
import { UserProvider } from "api/providers/user-provider";

const root = ReactDOM.createRoot(document.getElementById("root") as HTMLElement);
root.render(
   <QueryClientProvider client={queryClient}>
        <BrowserRouter>
            <UserProvider>
                <ModalBoxProvider>
                    <App />
                </ModalBoxProvider>
            </UserProvider>
        </BrowserRouter>
   </QueryClientProvider>
);

reportWebVitals();
