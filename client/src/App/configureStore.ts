import { configureStore } from "@reduxjs/toolkit";
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";
import { technologySlice } from "./technologySlice";
import { accountSlice } from "../Pages/Account/accountSlice";
import { testSlice } from "../Pages/TestPage/testSlice";
import { filterSlice } from "../Pages/StatisticsPage/filterSlice";

export const store = configureStore({
    reducer: {
        tech: technologySlice.reducer,
        account: accountSlice.reducer,
        test: testSlice.reducer,
        filter: filterSlice.reducer
    }
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;