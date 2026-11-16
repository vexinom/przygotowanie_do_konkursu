export default function Navbar() {
  return (
    <nav className="flex items-center justify-between bg-gray-900 px-8 py-4 text-white">
      <div className="text-xl font-bold">
        MathScroll
      </div>

      <div className="flex items-center gap-4">
        <button className="hover:text-gray-300">
          Home
        </button>

        <button className="hover:text-gray-300">
          Dashboard
        </button>

        <button className="hover:text-gray-300">
          Users
        </button>

        <button className="hover:text-gray-300">
          Settings
        </button>
      </div>

      <button className="rounded bg-red-600 px-4 py-2 hover:bg-red-700">
        Logout
      </button>
    </nav>
  );
}