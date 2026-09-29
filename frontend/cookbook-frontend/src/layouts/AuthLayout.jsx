import ThemeToggler from "@/components/ThemeToggler";
import { useTheme } from "@/hooks/useTheme";
import { Outlet } from "react-router";

function AuthLayout() {
  console.log("hello");

  const { theme, toggleTheme } = useTheme();

  return (
    <div className="min-h-dvh bg-neo-grid grid grid-cols-1 lg:grid-cols-2">
      <div className="flex flex-col items-center justify-center px-6 pt-28 pb-10 sm:px-8 lg:p-8">
        <div className="mb-0 max-w-xl text-center lg:mb-8 lg:text-left">
          <h1 className="mb-4 text-4xl font-black tracking-tight text-call-to-action sm:text-5xl lg:text-7xl">
            Cookbooklet
          </h1>

          <h2 className="mb-3 text-2xl font-bold tracking-tight text-call-to-action sm:text-3xl lg:text-4xl">
            Are you cooked?
          </h2>

          <p className="text-base leading-relaxed text-muted sm:text-lg lg:text-xl">
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
      <div className="flex items-center justify-center px-6 pb-12 sm:px-8 lg:mr-30 lg:p-0">
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
