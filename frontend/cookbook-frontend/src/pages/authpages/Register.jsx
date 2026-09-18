import { register } from "@/api/authApi/auth";
import { useState } from "react";
import { NavLink, useNavigate } from "react-router";

function Register() {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  async function handleRegister() {
    const trimmedName = username.trim();
    const trimmedEmail = email.trim();

    if (!trimmedName || !trimmedEmail || !password) return;

    try {
      await register(trimmedName, trimmedEmail, password);
      navigate("/login", { replace: true });
    } catch (error) {
      console.error(error);
      alert("Registration failed");
    }
  }

  return (
    <div>
      <form
        onSubmit={(e) => {
          e.preventDefault();
          handleRegister();
        }}
      >
        <fieldset className="fieldset bg-surface text-foreground rounded-2xl w-[32rem] border border-border p-10 shadow-xl">
          <div className="mb-5">
            <h1 className="text-3xl font-bold">Create an account</h1>
            <p className="mt-1 text-sm font-normal text-muted">
              Sign up to start using Cookbook
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
            Email
          </label>

          <input
            type="email"
            className="input input-lg w-full bg-surface-secondary border-border text-foreground"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
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
            Register
          </button>

          <p className="mt-3 text-center text-sm font-normal text-muted">
            Already have an account?
            <NavLink
              className="ml-1 font-semibold text-accent-color hover:underline"
              to="/login"
            >
              Log in
            </NavLink>
          </p>
        </fieldset>
      </form>
    </div>
  );
}

export default Register;
