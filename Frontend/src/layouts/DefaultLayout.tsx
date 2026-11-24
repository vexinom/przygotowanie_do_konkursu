import { Outlet } from "react-router";
import Navbar from "../components/Navbar";
import Sidebar from "../components/Sidebar";

export default function DefaultLayout() {
    return (
        <div className = "h-screen flex flex-row overflow-hidden">
            <Sidebar />
            
            <div className="flex min-w-0 flex-1 flex-col overflow-hidden">
                <Navbar/>

                    <main className=" mr-4 mb-4 min-w-0 flex flex-1 flex-col overflow-hidden text-white">

                        <div className="box-border flex-1 overflow-auto rounded-2xl bg-main-bg p-6">
                            <Outlet />  {/* Placeholder for child elements defined in AppRouter.tsx automatically replaced. */}
                        
                        </div>
                    </main>
            </div>
        </div>
    )
}