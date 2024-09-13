import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'

// https://vitejs.dev/config/
export default defineConfig({
  server: {
    port: 3004,
    open: 'http://localhost:3004'
  },
  plugins: [react()],
})
