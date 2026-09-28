import { Outlet } from "react-router";
import Navbar from "../components/Navbar";
import Footer from "../components/Footer";

function MainLayout() {
  return (
    <div className="min-h-dvh flex flex-col bg-neo-grid">
      <header className="w-full">
        <Navbar />
      </header>

      <main className="flex-1 mt-6 text-foreground">
        <Outlet />
      </main>

      <footer className="text-center bg-call-to-action">
        <Footer />
      </footer>
    </div>
  );
}

export default MainLayout;
