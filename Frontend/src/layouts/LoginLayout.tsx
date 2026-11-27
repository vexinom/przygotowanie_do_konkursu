import { Outlet } from "react-router";
import LoginForm from "../components/LoginForm";

export default function LoginLayout()
{
    return(
        <div className="flex min-h-dvh w-full p-0 py-4 sm:p-4 text-white">

            <div className="box-border flex flex-1 flex-h-dvh sm:min-h-[calc(100dvh-2rem)] rounded-2xl border border-main-bg">
                <LoginForm/>
                <Outlet/>
            </div>
        </div>
    )
}