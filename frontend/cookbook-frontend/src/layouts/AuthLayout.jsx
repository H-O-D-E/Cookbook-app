import ThemeToggler from "@/components/ThemeToggler";
import { useTheme } from "@/hooks/useTheme";
import { Outlet } from "react-router";

function AuthLayout() {
  const { theme, toggleTheme } = useTheme();

  return (
    <div className="min-h-dvh bg-neo-grid grid grid-cols-1 lg:grid-cols-2">
      <div className="flex flex-col items-center justify-center p-8">
        <h1 className="text-6xl font-bold mb-6 text-black">I'm Cooked</h1>
        <h2 className="text-black text-2xl">
          Create your own cookbook and share it with the world!
        </h2>
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
