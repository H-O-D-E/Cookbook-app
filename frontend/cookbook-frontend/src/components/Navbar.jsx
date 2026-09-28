import { useNavigate, NavLink } from "react-router";
import { clearToken } from "@/auth/token";
import { useTheme } from "@/hooks/useTheme";
import ThemeToggler from "./ThemeToggler";
import { useQueryClient } from "@tanstack/react-query";

function Navbar() {
  const navigate = useNavigate();
  const { theme, toggleTheme } = useTheme();
  const queryClient = useQueryClient();

  function handleLogout() {
    clearToken();
    queryClient.clear();
    navigate("/login", { replace: true });
  }

  return (
    <nav className="grid grid-cols-3 items-center px-4 bg-base-100 h-16">
      <div className="flex justify-start">
        <NavLink to="/" className="text-2xl font-bold ">
          Cookbooklet
        </NavLink>
      </div>

      <div className="flex justify-center gap-6 font-semibold text-2xl ">
        <NavLink to="/">My cookbooks</NavLink>
        <NavLink to="/explore">Explore</NavLink>
      </div>

      <div className="flex justify-end items-center gap-3">
        <div className="nav-options">
          <ThemeToggler theme={theme} toggleTheme={toggleTheme} />
          <button
            className="btn btn-ghost text-lg lg:text-2xl"
            onClick={handleLogout}
          >
            Sign out
          </button>
        </div>
      </div>
    </nav>
  );
}

export default Navbar;
