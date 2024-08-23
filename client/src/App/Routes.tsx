import { createBrowserRouter } from "react-router-dom";
import App from "./App";
import AboutPage from "../Pages/AboutPage/AboutPage";
import ProfilePage from "../Pages/ProfilePage/ProfilePage";
import StatisticsPage from "../Pages/StatisticsPage/StatisticsPage";
import HomePage from "../Pages/HomePage/HomePage";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            {path: '', element: <HomePage />},
            {path: 'test', element: <HomePage />},
            {path: 'statistics', element: <StatisticsPage />},
            {path: 'profile', element: <ProfilePage />},
            {path: 'about', element: <AboutPage />},
            {path: 'signin', element: <HomePage />},
            {path: 'signup', element: <HomePage />},
            {path: 'signout', element: <HomePage />},
        ]
    }
]);