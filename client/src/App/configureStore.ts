import { configureStore } from "@reduxjs/toolkit";
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";
import { technologySlice } from "./technologySlice";
import { statisticsSlice } from "./statisticsSlice";
import { globalStateSlice } from "./globalStateSlice";

export const store = configureStore({
    reducer: {
        tech: technologySlice.reducer,
        tops: statisticsSlice.reducer,
        globalState: globalStateSlice.reducer
    }
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;