import { apiFetch } from "../apiFetch";

export async function login(name, password) {
  const res = await apiFetch("/api/auth/login", {
    method: "POST",
    body: JSON.stringify({ name, password }),
  });

  if (!res.ok) throw new Error("Wrong username or password, maybe both lol");
  return res.json();
}

export async function register(name, email, password) {
  const res = await apiFetch("/api/auth/register", {
    method: "POST",
    body: JSON.stringify({
      name,
      email,
      password,
    }),
  });

  if (!res.ok) {
    throw new Error("Registration failed");
  }

  return;
}
