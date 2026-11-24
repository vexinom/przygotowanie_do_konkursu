import { Outlet } from "react-router";
import LoginForm from "../components/LoginForm";

export default function LoginLayout()
{
    return(
        <div className="flex h-screen w-screen overflow-hidden p-4 text-white">

            <div className="box-border flex-1 overflow-auto rounded-2xl border border-main-bg">
                <LoginForm/>
                <Outlet/>
            </div>
        </div>
    )
}