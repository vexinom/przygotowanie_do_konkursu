import { BrowserRouter, Route, Routes } from "react-router";
import HomePage from "../pages/HomePage.tsx"
import ScrollPage from "../pages/ScrollPage.tsx"
import DefaultLayout from "../layouts/DefaultLayout.tsx";

export default function AppRouter() {
    return (
        <BrowserRouter>
            <Routes>
                <Route element={<DefaultLayout />}>
                    <Route index element={<HomePage />}></Route>
                    <Route path="scroll" element={<ScrollPage />}></Route>
                </Route>
            </Routes>
        </BrowserRouter>
    )
}