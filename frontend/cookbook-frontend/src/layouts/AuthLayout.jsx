import ThemeToggler from "@/components/ThemeToggler";
import { useTheme } from "@/hooks/useTheme";
import { Outlet } from "react-router";

function AuthLayout() {
  const { theme, toggleTheme } = useTheme();

  return (
    <div className="min-h-dvh bg-neo-grid grid grid-cols-1 lg:grid-cols-2">
      <div className="flex flex-col items-center justify-center p-8">
        <div className="mb-8 max-w-xl text-center lg:text-left">
          <h1 className="mb-4 text-6xl font-black tracking-tight text-foreground lg:text-7xl">
            Cookbooklet
          </h1>

          <h2 className="mb-3 text-3xl font-bold tracking-tight text-foreground lg:text-4xl">
            No idea what to cook?
          </h2>

          <p className="text-lg leading-relaxed text-muted lg:text-xl">
            Discover cookbooks from people around the world, find recipes you
            love, and create cookbooks of your own.
          </p>
        </div>
        <img
          src="/LoginPicture.svg"
          alt=""
          className="hidden lg:block w-full max-w-2xl h-auto"
        />
      </div>
      <div className="flex items-center justify-center px-6 pb-8 lg:mr-30 lg:p-0">
        <Outlet />
      </div>

      <div className="fixed top-5 right-5 m-2">
        <button className="btn btn-circle btn-xl">
          <ThemeToggler theme={theme} toggleTheme={toggleTheme} />
        </button>
      </div>
    </div>
  );
}

export default AuthLayout;
