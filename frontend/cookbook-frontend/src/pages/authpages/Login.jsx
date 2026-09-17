import { login } from "@/api/authApi/auth";
import { setToken } from "@/auth/token";
import { useState } from "react";
import { NavLink, useNavigate } from "react-router";

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  async function handleLogin() {
    const trimmedName = username.trim();

    if (!trimmedName || !password) return;

    try {
      const res = await login(trimmedName, password);
      setToken(res.token);
      navigate("/", { replace: true });
    } catch (error) {
      console.error(error);
      alert("Feil brukernavn eller passord");
    }
  }

  return (
    <div>
      <form
        onSubmit={(e) => {
          e.preventDefault();
          handleLogin();
        }}
      >
        <fieldset className="fieldset bg-surface text-foreground rounded-2xl w-lg border border-border p-10 shadow-xl">
          <div className="mb-5">
            <h1 className="text-3xl font-bold">Welcome back</h1>
            <p className="mt-1 text-sm font-normal text-muted">
              Sign in to continue to Cookbook
            </p>
          </div>

          <label className="label font-semibold text-foreground">
            Username
          </label>

          <input
            type="text"
            className="input input-lg w-full bg-surface-secondary border-border text-foreground"
            placeholder="Username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            required
          />

          <label className="label mt-3 font-semibold text-foreground">
            Password
          </label>

          <input
            type="password"
            className="input input-lg w-full bg-surface-secondary border-border text-foreground"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />

          <button
            className="btn mt-7 w-full border-0 bg-call-to-action text-white"
            type="submit"
          >
            Login
          </button>

          <p className="mt-3 text-center text-sm font-normal text-muted">
            New user?
            <NavLink
              className="ml-1 font-semibold text-accent-color hover:underline"
              to="/register"
            >
              Create an account
            </NavLink>
          </p>
        </fieldset>
      </form>
    </div>
  );
}

export default Login;
