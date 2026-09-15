import { Outlet } from "react-router";
import Navbar from "../components/Navbar";
import Footer from "../components/Footer";
import { UserPlus } from "lucide-react";

function MainLayout() {
  return (
    <div className="bg-blue-200 min-h-dvh flex center-items justify-center">
      <header>
        <Navbar />
        <UserPlus />
      </header>

      <main>
        <Outlet />
      </main>

      <footer>
        <Footer />
      </footer>
    </div>
  );
}

export default MainLayout;
