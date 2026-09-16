//Use this method for every api fetch for standarization

import { getToken } from "@/auth/token";

//denne env var ligger i .env.local
const API_BASE_URL = import.meta.env.VITE_API_URL;

export async function apiFetch(path, init = {}) {
  const token = getToken();

  const headers = new Headers(init.headers);

  if (token) {
    headers.set("Authorization", `Bearer ${token}`);
  }

  if (init.body && !headers.has("Content-Type")) {
    headers.set("Content-Type", "application/json");
  }

  const res = await fetch(`${API_BASE_URL}${path}`, {
    ...init,
    headers,
  });

  if (res.status === 401) {
    clearToken();
    window.location.href = "/login";
    throw new Error("Unauthorized");
  }

  return res;
}
