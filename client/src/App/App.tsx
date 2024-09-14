import { Container, createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import { Outlet } from "react-router-dom";
import ResponsiveAppBar from "./ResponsiveAppBar";
import { useEffect } from "react";
import { getTechnologiesAsync } from "./technologySlice";
import { fetchCurrentUser } from "../Pages/Account/accountSlice";
import { useDispatch } from "react-redux";
import { AppDispatch } from "./configureStore";
import { ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';

function App() {
  const dispatch = useDispatch<AppDispatch>();

  useEffect(() => {
    dispatch(getTechnologiesAsync());
    dispatch(fetchCurrentUser());
  }, []);

  return (
    <>
      <CssBaseline />
      <ToastContainer position="bottom-right" hideProgressBar theme="colored" />
      <ResponsiveAppBar />
      <Container>
        <Outlet />
      </Container>
    </>
  )
}

export default App
