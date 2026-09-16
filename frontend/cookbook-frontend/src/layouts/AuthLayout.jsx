import ThemeToggler from "@/components/ThemeToggler";
import { useTheme } from "@/hooks/useTheme";
import { Outlet } from "react-router";

function AuthLayout() {
  const { theme, toggleTheme } = useTheme();

  return (
    <div className="flex min-h-dvh items-center justify-center  bg-neo-grid">
      <div className="fixed top-5 right-5 m-2">
        <button className="btn btn-circle btn-xl">
          <ThemeToggler theme={theme} toggleTheme={toggleTheme} />
        </button>
      </div>
      <Outlet />
    </div>
  );
}

export default AuthLayout;
