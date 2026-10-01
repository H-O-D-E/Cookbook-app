import { useNavigate, NavLink } from "react-router";
import { clearToken } from "@/auth/token";
import { useTheme } from "@/hooks/useTheme";
import ThemeToggler from "./ThemeToggler";
import { useQueryClient } from "@tanstack/react-query";
import { apiFetch } from "@/api/apiFetch";

function Navbar() {
  const navigate = useNavigate();
  const { theme, toggleTheme } = useTheme();
  const queryClient = useQueryClient();

  async function handleLogout() {
    try {
      await apiFetch("/api/auth/logout", { method: "POST" });
    } finally {
      clearToken();
      queryClient.clear();
      navigate("/login", { replace: true });
    }
  }

  return (
    <nav className="grid grid-cols-2 items-center gap-y-2 bg-base-100 px-4 py-3 lg:h-16 lg:grid-cols-3 lg:py-0">
      <div className="flex justify-start">
        <NavLink to="/" className="text-lg font-bold sm:text-2xl">
          Cookbooklet
        </NavLink>
      </div>

      <div className="order-3 col-span-2 flex justify-center gap-6 text-base font-semibold sm:text-lg lg:order-none lg:col-span-1 lg:text-2xl">
        <div className="hover:underline">
          <NavLink to="/">My cookbooks</NavLink>
        </div>
        <div className="hover:underline cursor-pointer">
          <NavLink to="/explore">Explore</NavLink>
        </div>
      </div>

      <div className="flex items-center justify-end gap-3">
        <div className="nav-options">
          <ThemeToggler
            title="Change light theme"
            theme={theme}
            toggleTheme={toggleTheme}
          />
          <button
            className="btn btn-ghost px-2 text-sm sm:text-lg lg:text-2xl"
            onClick={handleLogout}
            title="Sign out button"
          >
            Sign out
          </button>
        </div>
      </div>
    </nav>
  );
}

export default Navbar;
