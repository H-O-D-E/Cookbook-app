import { Outlet } from "react-router";
import Navbar from "../components/Navbar";
import Footer from "../components/Footer";

function MainLayout() {
  return (
    <div className="grid grid-rows-[3%_90%] bg-neo-grid">
      <div className="flex items-top justify-center">
        <header className="w-full  ">
          <Navbar />
        </header>
      </div>
      <div>
        <main className="min-h-dvh mt-6 text-foreground">
          <Outlet />
        </main>
      </div>
      <footer className="text-center bg-call-to-action ">
        <Footer />
      </footer>
    </div>
  );
}

export default MainLayout;
