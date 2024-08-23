import { Container, CssBaseline } from "@mui/material";
import { Outlet } from "react-router-dom";
import ResponsiveAppBar from "./ResponsiveAppBar";
import { useEffect } from "react";
import { getTechnologiesAsync } from "./technologySlice";
import { useDispatch } from "react-redux";
import { AppDispatch } from "./configureStore";

function App() {
  const dispatch = useDispatch<AppDispatch>();

  useEffect(() => {
    dispatch(getTechnologiesAsync());
}, []);

  return (
    <>
      <CssBaseline />
      <ResponsiveAppBar />
      <Container>
        <Outlet />
      </Container>
    </>
  )
}

export default App
