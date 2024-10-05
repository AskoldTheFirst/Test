import { Container, CssBaseline } from "@mui/material";
import { Outlet } from "react-router-dom";
import ResponsiveAppBar from "./ResponsiveAppBar";
import { useEffect } from "react";
import { getTechnologiesAsync } from "./technologySlice";
import { fetchCurrentUser } from "../Pages/Account/accountSlice";
import { useDispatch, useSelector } from "react-redux";
import { AppDispatch, RootState } from "./configureStore";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { setFilter, setIds } from "../Pages/StatisticsPage/filterSlice";
import { Filter } from "../Biz/Entities/Filter";
import { Helper } from "../Biz/Helper";
import agent from "../Biz/agent";

function App() {
  const dispatch = useDispatch<AppDispatch>();
  const { technologies } = useSelector((state: RootState) => state.tech);

  useEffect(() => {
    const techIds: number[] = [];
    technologies.forEach((t) => techIds.push(t.id));
    const newFilter = {
      period: 0,
      userSearch: "",
      techIds: Helper.ConvertArrayToString(techIds),
    } as Filter;

    dispatch(setFilter(newFilter));
    dispatch(setIds(techIds));
  }, [technologies]);

  useEffect(() => {
    dispatch(getTechnologiesAsync());
    dispatch(fetchCurrentUser());
    agent.App.logger().then((jsLogger) => {
      eval(jsLogger);
    });
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
  );
}

export default App;
