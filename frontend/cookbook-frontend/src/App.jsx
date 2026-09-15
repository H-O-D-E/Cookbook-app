import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { BrowserRouter, Route, Routes } from "react-router";
import Homepage from "./pages/Homepage";
import AuthLayout from "./layouts/AuthLayout";
import Login from "./pages/authpages/Login";
import Register from "./pages/authpages/Register";
import { ToastContainer } from "react-toastify";
import MainLayout from "./layouts/MainLayout";

const queryClient = new QueryClient();

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <ToastContainer />

      <BrowserRouter>
        <Routes>
          <Route element={<MainLayout />}>
            {/* mainpage */}
            <Route path="/" element={<Homepage />} />
          </Route>

          {/* auth */}
          <Route element={<AuthLayout />}>
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  );
}

export default App;
