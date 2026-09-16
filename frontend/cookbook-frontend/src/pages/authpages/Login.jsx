import { login } from "@/api/authApi/auth";
import { setToken } from "@/auth/token";
import { useState } from "react";
import { useNavigate } from "react-router";

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
          <label className="label text-black">Password</label>
          <input
            type="password"
            className="input"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
          <p className="font-bold text-black">
            New user?
            <span className="pl-2 underline">click here</span>
          </p>
          <button className="btn btn-neutral mt-4" type="submit">
            Login
          </button>
        </fieldset>
      </form>
    </div>
  );
}

export default Login;
