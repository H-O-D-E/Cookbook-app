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
        <fieldset className="fieldset bg-accent-color font-bold border-base-300 rounded-box w-xs border p-4">
          <label className="label text-black">Username</label>
          <input
            type="text"
            className="input"
            placeholder="Username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            required
          />

          <label className="label text-black">Email</label>
          <input
            type="email"
            className="input"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />

          <label className="label text-black">Password</label>
          <input
            type="password"
            className="input"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
          <p className="text-black">
            Already have a user?
            <span className="underline pl-2">
              <NavLink to="/login">Log in here</NavLink>
            </span>
          </p>

          <button className="btn btn-neutral mt-4" type="submit">
            Register
          </button>
        </fieldset>
      </form>
    </div>
  );
}

export default Register;
