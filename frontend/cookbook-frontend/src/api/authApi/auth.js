import { apiFetch } from "../apiFetch";

export async function login(username, password) {
  const res = await apiFetch("/api/auth/login", {
    method: "POST",
    body: JSON.stringify({
      username,
      password,
    }),
  }, false);

  if (!res.ok) {
    const problem = await res.json().catch(() => null);
    const validationMessages = Object.values(problem?.errors ?? {})
      .flatMap((value) => (Array.isArray(value) ? value : [value]))
      .filter((value) => typeof value === "string" && value.trim());

    const message =
      validationMessages.join(" ") ||
      problem?.detail ||
      problem?.title ||
      "Login failed";

    throw new Error(message);
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
  }, false);

   if (!res.ok) {
    const problem = await res.json().catch(() => null);
    const validationMessages = Object.values(problem?.errors ?? {})
      .flatMap((value) => (Array.isArray(value) ? value : [value]))
      .filter((value) => typeof value === "string" && value.trim());

    const message =
      validationMessages.join(" ") ||
      problem?.detail ||
      problem?.title ||
      "Registration failed";

    throw new Error(message);
  }
}
