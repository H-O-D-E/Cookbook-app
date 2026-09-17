import { Outlet } from "react-router";
import Navbar from "../components/Navbar";
import Footer from "../components/Footer";
import { UserPlus } from "lucide-react";
import { useState } from "react";
// Importeres med <UserPlus />

function MainLayout() {
  return (
    <div className="grid grid-rows-[3%_90%] bg-neo-grid ">
      <div className=" min-h-dvh flex items-top justify-center">
        <header className="w-full ">
          <Navbar />
        </header>
      </div>
      <div>
        <main>
          <Outlet />
        </main>

        <footer>
          <Footer />
        </footer>
      </div>
    </div>
  );
}

export default MainLayout;
