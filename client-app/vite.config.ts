import { defineConfig } from "vite";
import react from "@vitejs/plugin-react-swc";

/*
    We can define our application port here
    We will change this React App server to run at app port 3000 instead od the default: 5173
    server: {
    port: 3000,
    },
*/
// https://vitejs.dev/config/
export default defineConfig({
  server: {
    port: 3000,
  },
  plugins: [react()],
});
