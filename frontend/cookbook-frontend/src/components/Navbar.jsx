import { Link, replace, useNavigate } from "react-router";
import { Book } from "lucide-react";
import Login from "../pages/authpages/Login";
import { clearToken } from "@/auth/token";
import { useTheme } from "@/hooks/useTheme";
import ThemeToggler from "./ThemeToggler";

function Navbar() {
  const navigate = useNavigate();
  const { theme, toggleTheme } = useTheme();

  function handleLogout() {
    clearToken();
    navigate("/login", { replace: true });
  }

  return (
    <nav className="grid grid-cols-3 items-start px-4 pt-3 bg-base-100 h-16">
      <div className="flex justify-start">
        <Link to="/" className=" text-2xl font-bold">
          Cookbooklet
        </Link>
      </div>
      <div />
      <div className="flex gap-3 justify-end items-center">
        <div className="nav-options">
          <Link to="/recipebooks" className="btn btn-ghost">
            <Book />
          </Link>
          <ThemeToggler theme={theme} toggleTheme={toggleTheme} />
          <button className="btn btn-ghost" onClick={handleLogout}>
            Log out
          </button>
        </div>
      </div>
    </nav>
  );
}

export default Navbar;
