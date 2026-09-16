import react, { reactCompilerPreset } from "@vitejs/plugin-react";
import babel from "@rolldown/plugin-babel";
import { defineConfig } from "vite";
import tailwindcss from "@tailwindcss/vite";
import { fileURLToPath, URL } from "node:url";

// REVIEW(azure): when you start calling the API, do not hardcode http://localhost:5217 in the components and do not add dotenv. Vite has this built in: put VITE_API_BASE_URL in .env.development and .env.production, read it as import.meta.env.VITE_API_BASE_URL, and add a server.proxy block here for local development so the browser sees same-origin requests and CORS never comes up while you work.
// REVIEW(noob): anything you put in a VITE_ variable is baked into the built bundle and readable by anyone. It is fine for an API base URL, never for a secret.
// https://vite.dev/config/
export default defineConfig({
  plugins: [
    tailwindcss(),
    react(),
    babel({ presets: [reactCompilerPreset()] }),
  ],

  resolve: {
    alias: {
      "@": fileURLToPath(new URL("./src", import.meta.url)),
    },
  },
});
