import { useState, type FormEvent } from "react";

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
        <div className="flex w-full h-full rounded-2xl bg-main-bg">

            <div className="w-1/2 p-6">
                <div className="flex h-full w-full flex-col items-center justify-center rounded-2xl border border-main-border p-4 "> 
                    
                    <div className=" w-3/4 h-3/5 justify-center flex flex-col rounded-2xl bg-white/10 border border-white/10">
                        
                        <form onSubmit={handleSubmit} className="mt-20 flex flex-col gap-4 ">

                            <div className="flex flex-col items-center gap-1.5 ">

                                <label htmlFor="email" className="w-3/4 text-left text-sm font-bold text-gray-300">Email</label>
                                <div className="flex h-10 w-3/4 rounded-xl border border-white/40 bg-gradient-to-b from-[#727272]/20 to-[#D8D8D8]/20">

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

                                <label htmlFor="username" className="w-3/4 text-left text-sm font-bold text-gray-300">Password</label>

                                <div className="flex h-10 w-3/4 rounded-xl border border-white/40 bg-gradient-to-b from-[#727272]/20 to-[#D8D8D8]/20">

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

                            <div className="mx-auto mt-7 flex w-3/4">
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


                            <div className="mx-auto flex w-3/4 justify-end">
                                <button
                                    type="submit"
                                    className="mt-15 w-3/5 rounded-xl bg-button px-4 py-2 hover:bg-button-hover border border-white/20 font-bold">
                                        Log in
                                </button>
                            </div>

                            
                        </form>

                        

                        {message && <p className="mx-auto w-3/4 text-sm font-bold text-gray-200">{message}</p>}


                        

                    </div>

                    <div className="mt-4 -mr-10 w-3/4 flex flex-col font-bold text-gray-300">
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

            
        </div>
        
    );
}