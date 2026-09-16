import { Outlet } from "react-router";

function AuthLayout() {
  return (
    <div className="flex min-h-dvh items-center justify-center  bg-neo-grid">
      <Outlet />
    </div>
  );
}

export default AuthLayout;
