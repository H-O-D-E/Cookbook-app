import { Navigate, Outlet, useLocation } from "react-router";
import { getToken } from "./token";

export default function ProtectedRoute() {
  const token = getToken();
  const location = useLocation();

  if (!token) {
    return <Navigate to="/login" replace state={{ from: location }} />;
  }

  return <Outlet />;
}
