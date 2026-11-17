export default function Navbar() {
  return (
    <nav className="flex items-center justify-between bg-nav px-8 py-4 text-white">
      <div className="text-xl font-bold">
        MathScroll
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
        <button className="rounded bg-button px-4 py-2 hover:bg-button-hover">
          YY
        </button>

        <button className="rounded bg-button px-4 py-2 hover:bg-button-hover">
          Logout
        </button>

      </div>
    </nav>
  );
}