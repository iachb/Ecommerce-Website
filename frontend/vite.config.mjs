import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import tailwindcss from "@tailwindcss/vite";

// ponytail: no config beyond plugins; Tailwind v4 needs no tailwind.config.js
// .mjs so the ESM-only @tailwindcss/vite plugin loads without a package "type" change
export default defineConfig({
  plugins: [react(), tailwindcss()],
});
