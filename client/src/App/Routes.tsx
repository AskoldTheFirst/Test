import { createBrowserRouter, Navigate } from "react-router-dom";
import App from "./App";
import AboutPage from "../Pages/AboutPage/AboutPage";
import ProfilePage from "../Pages/ProfilePage/ProfilePage";
import StatisticsPage from "../Pages/StatisticsPage/StatisticsPage";
import HomePage from "../Pages/HomePage/HomePage";
import TestCommencePage from "../Pages/TestPage/TestCommencePage";
import TestPage from "../Pages/TestPage/TestPage";
import TestResultPage from "../Pages/TestPage/TestResultPage";
import Login from "../Pages/Account/Login";
import Register from "../Pages/Account/Register";
import NotFound from "../Pages/Errors/NotFound";
import ServerError from "../Pages/Errors/ServerError";
import RequireAuth from "./RequireAuth";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            {element: <RequireAuth />, children: [
                {path: 'test', element: <TestPage />},
                {path: 'test-result', element: <TestResultPage />},
                {path: 'profile', element: <ProfilePage />},
                {path: 'statistics', element: <StatisticsPage />},
            ]},
            {path: '', element: <HomePage />},
            {path: 'home', element: <HomePage />},
            {path: 'statistics', element: <StatisticsPage />},
            {path: 'profile', element: <ProfilePage />},
            {path: 'about', element: <AboutPage />},
            {path: 'login', element: <Login />},
            {path: 'register', element: <Register />},
            {path: 'signout', element: <HomePage />},
            {path: 'commenceTest/:testId', element: <TestCommencePage />},
            {path: 'test', element: <TestPage />},
            {path: 'test-result', element: <TestResultPage />},
            {path: 'server-error', element: <ServerError />},
            {path: 'not-found', element: <NotFound />},
            {path: '*', element: <Navigate replace to='not-found' />},
        ]
    }
]);