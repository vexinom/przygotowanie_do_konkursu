export default function Sidebar() {
  return (
    <aside className= "mx-4 mt-4 mb-4 rounded-2xl w-64 p-4 flex flex-col bg-side-nav text-white">

        <div className="mb-4 flex h-24 w-full items-center justify-center bg-black rounded-2xl text-3xl font-bold"> 
          MathScroll
        </div>
          
        <nav>
            <button className="px-4 py-2 rounded hover:bg-nav-button-hover flex-col flex">
              Home
            </button>

            <button className="px-4 py-2 rounded hover:bg-nav-button-hover flex-col flex
            ">
              Dashboard
            </button>

            <button className="px-4 py-2 rounded hover:bg-nav-button-hover flex-col flex
            ">
              Dashboard
            </button>


            <button className="px-4 py-2 rounded hover:bg-nav-button-hover flex-col flex
            ">
              Dashboard
            </button>
        </nav>
  </aside>
  );
}