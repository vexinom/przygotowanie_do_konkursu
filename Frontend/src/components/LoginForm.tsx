import { useState, type FormEvent } from "react";
import avatarImg from "../assets/avatar_icon.png"
import treeImg from "../assets/tree.png"

const API_URL = "https://localhost:7147/api/Auth/login";

export default function LoginForm()
{
    const[email, setEmail] = useState("");
    const[password, setPassword] = useState("");
    const[message, setMessage] = useState("");
    const[isLoggedIn, setIsLoggedIn] = useState(false);

    async function handleSubmit(e: FormEvent<HTMLFormElement>) {
        e.preventDefault();

        try
        {
            const response = await fetch(API_URL, 
                {
                    method: "POST",
                    headers: {"Content-type": "application/json"},
                    credentials: "include",
                    body: JSON.stringify({ email: email, password })   
                }
            );
            const data = await response.json().catch(() => null);

            if(!response.ok)
            {
                setMessage(data?.message || "Login failed")
                return;
            }
            setIsLoggedIn(true);
        
        }
        catch
        {
            setMessage("Connection error")
        }
    }

    if(isLoggedIn)
    {
        return(
            <div className="flex h-full w-full flex-col items-center justify-center gap-4 rounded-2xl bg-main-bg p-6 text-white">
                <h1 className="text-2xl font-bold">Testowa strona po zalogowaniu</h1>
                <p>Zalogowano jako: {email}</p>
                <button
                    type="button"
                    onClick={() => {
                        setIsLoggedIn(false);
                        setPassword("");
                        setMessage("");
                    }}
                    className="rounded-xl bg-button px-4 py-2 font-bold border border-white/20"
                >
                    Wróć do logowania
                </button>
            </div>
        );
    }

    return (
        <div className="flex w-full min-h-full flex-col items-stretch lg:flex-row rounded-2xl bg-main-bg">

            <div className="flex w-full py-3 sm:p-6 lg:w-1/2">

                <div className="flex w-full flex-col items-center justify-center rounded-none sm:rounded-2xl sm:border sm:border-main-border py-3 sm:p-4 "> 
                    
                    <div className="relative w-full sm:w-3/4 mt-14 py-8 flex-col rounded-2xl bg-white/10 border border-white/10">
                       
                       <div className="absolute -top-12 left-1/2 h-24 w-24 -translate-x-1/2 overflow-hidden rounded-full border border-white/40 bg-[#2b2727]">
                            <img
                                src={avatarImg}
                                alt=""
                                className="h-full w-full object-cover"
                            />
                       </div>


                        {/*Form to log in */}
                        <form onSubmit={handleSubmit} className="sm:mt-20 mt-14 flex flex-col gap-4 ">

                            <div className="flex flex-col items-center gap-1.5 ">

                                <label htmlFor="email" className="w-full sm:w-3/4 text-left text-sm font-bold text-gray-300">Email</label>
                                
                                <div className="flex h-10 w-full sm:w-3/4 rounded-xl border border-white/40 bg-gradient-to-b from-[#727272]/20 to-[#D8D8D8]/20">

                                    <div className="flex w-11 shrink-0 items-center justify-center border-r border-white/10 bg-[#9b9b9b] rounded-tl-xl rounded-bl-xl">
                                        <i className="icon-user text-black text-2xl"></i>
                                    </div>
                                    <input
                                        id="email"
                                        name="email"
                                        type="text"
                                        value={email}
                                        onChange={(e) => setEmail(e.target.value)}
                                        required
                                        placeholder="Enter username"
                                        className="h-full w-full bg-transparent p-4"
                                    />
                                </div>
                                
                            </div>

                            <div className="flex flex-col items-center gap-1.5 ">

                                <label htmlFor="password" className="w-full sm:w-3/4 text-left text-sm font-bold text-gray-300">Password</label>

                                <div className="flex h-10 w-full sm:w-3/4 rounded-xl border border-white/40 bg-gradient-to-b from-[#727272]/20 to-[#D8D8D8]/20">

                                    <div className="flex w-11 shrink-0 items-center justify-center border-r border-white/10 bg-[#9b9b9b] rounded-tl-xl rounded-bl-xl">
                                        <i className="icon-lock text-black text-2xl" aria-hidden="true"></i>
                                    </div>
                                    <input
                                        id="password"
                                        name="password"
                                        type="password"
                                        value={password}
                                        onChange={(e) => setPassword(e.target.value)}
                                        required
                                        placeholder="Enter password"
                                        className="h-full w-full bg-transparent p-4"
                                    />
                                </div>
                                
                            </div>

                            <div className="mx-auto mt-7 flex w-11/12 sm:w-3/4 gap-2.5">
                                <input
                                    id="remember"
                                    name="remember"
                                    type="checkbox"
                                    className="h-5 w-5 cursor-pointer rounded-md bg-[#9b9b9b]"
                                />
                                <label
                                    htmlFor="remember"
                                    className="text-sm font-semibold text-gray-300"
                                >
                                    Remember me
                                </label>
                        
                            </div>

                            <p
                                role="alert"
                                aria-live="polite"
                                className={`mx-auto min-h-5 w-3/4 text-sm font-bold text-rose-300 transition-opacity ${
                                    message ? "opacity-100" : "opacity-0"
                                }`}
                            >
                                {message || " "}
                            </p>

                            
                            
                            <div className="mx-auto flex w-3/4 justify-end">
                                <button
                                    type="submit"
                                    className="mt-10 w-3/5 rounded-xl bg-button px-4 py-2 hover:bg-button-hover border border-white/20 font-bold">
                                        Log in
                                </button>
                            </div>

                            
                        </form>

                        

                        


                        

                    </div>

                    <div className="mt-4 sm:-mr-10 w-3/4 flex flex-col items-center sm:items-start text-center sm:text-left font-bold text-gray-300">
                    <a href="/forgot-password">
                        I don't remember password
                    </a>
                       
                    </div>

                    <div className="mt-4 w-3/4 flex flex-col">
                        <button
                            className="mx-auto mt-25 w-3/4 rounded-xl bg-button px-4 py-2 hover:bg-button-hover border border-white/20 font-bold">
                            Register
                        </button>
                    </div>


                </div>
            </div>

            <div className="relative hidden flex-1 self-stretch p-6 lg:block lg:w-1/2">


                <img
                        src={treeImg}
                        alt=""
                        className="pointer-events-none absolute bottom-0 left-1/2 max-h-[65%] max-w-full -translate-x-1/2 object-contain object-bottom"
                />
            </div>

            
        </div>
        
    );
}