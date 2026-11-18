import { Outlet } from "react-router";
import Navbar from "../components/Navbar";
import Sidebar from "../components/Sidebar";

export default function DefaultLayout() {
    return (
        <div className = "h-screen flex flex-col overflow-hidden">
            <Navbar />
            
            <div className="flex flex-1 overflow-hidden">
                <Sidebar/>
                <main className="p-8 flex-1">
                    <Outlet />  {/* Placeholder for child elements defined in AppRouter.tsx automatically replaced. */}
                </main>
            </div>
        </div>
    )
}