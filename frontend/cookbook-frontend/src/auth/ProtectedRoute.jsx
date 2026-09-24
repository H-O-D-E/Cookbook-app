import { Navigate, Outlet, useLocation } from "react-router";
import { clearToken, getToken, isTokenExpired } from "./token";

export default function ProtectedRoute() {
  const token = getToken();
  const location = useLocation();

  if (!token || isTokenExpired(token)) {
    clearToken();

    return <Navigate to="/login" replace state={{ from: location }} />;
  }

  return <Outlet />;
}
