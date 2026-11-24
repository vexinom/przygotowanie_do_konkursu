import { useNavigate } from "react-router";


export default function Navbar() {
  const navigate = useNavigate();
  return (
    <nav className="mt-4 mb-4 mr-4 ml-0 h-24 rounded-2xl flex items-center justify-between bg-nav px-8 py-4 text-white text-xl font-semibold">
      <div className="text-xl font-bold">
        
      </div>

      <div className="flex items-center" >
        <button className="px-4 py-2 rounded hover:bg-nav-button-hover">
          Home
        </button>

        <button className="px-4 py-2 rounded hover:bg-nav-button-hover">
          Dashboard
        </button>

        <button className="px-4 py-2 rounded hover:bg-nav-button-hover">
          Users
        </button>

        <button className="px-4 py-2 rounded hover:bg-nav-button-hover">
          Settings
        </button>
      </div>

      <div className = "flex items-center gap-3">
        

        <button 
        onClick={() => navigate("/login")}
        className="rounded bg-button px-4 py-2 hover:bg-button-hover">
          Login
        </button>

      </div>
    </nav>
  );
}