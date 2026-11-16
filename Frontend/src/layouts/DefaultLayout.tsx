import { Outlet } from "react-router";
import Navbar from "../components/Navbar";

export default function DefaultLayout() {
    return (
        <>
            <div>
                <Navbar />
            </div>
            <div>
                <Outlet />  {/* Placeholder for child elements defined in AppRouter.tsx automatically replaced. */}
            </div>
        </>
    )
}