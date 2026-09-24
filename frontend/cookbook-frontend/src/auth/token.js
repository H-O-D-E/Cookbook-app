import { Navigate, Outlet, useLocation } from "react-router";

export function getToken() {
  return localStorage.getItem("token");
}

export function setToken(token) {
  localStorage.setItem("token", token);
}

export function clearToken() {
  localStorage.removeItem("token");
}

export function isTokenExpired(token) {
  if (!token) return true;

  try {
    const payload = JSON.parse(atob(token.split(".")[1]));

    if (typeof payload.exp !== "number") {
      return true;
    }

    return Date.now() >= payload.exp * 1000;
  } catch {
    return true;
  }
}
