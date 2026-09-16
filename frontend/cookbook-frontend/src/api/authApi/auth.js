import { apiFetch } from "../apiFetch";

export async function login(username, password) {
  const res = await apiFetch("/api/auth/login", {
    method: "POST",
    body: JSON.stringify({
      username,
      password,
    }),
  });

  if (!res.ok) {
    throw new Error("Wrong username or password");
  }

  return res.json();
}

export async function register(username, email, password) {
  const res = await apiFetch("/api/auth/register", {
    method: "POST",
    body: JSON.stringify({
      username,
      email,
      password,
    }),
  });

  if (!res.ok) {
    throw new Error("Registration failed");
  }
}
