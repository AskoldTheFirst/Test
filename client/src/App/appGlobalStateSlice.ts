import { createSlice } from "@reduxjs/toolkit";
import { AppGlobalStateEntity } from "../Biz/Entities/AppGlobalStateEntity";

export interface AppGlobalState {
    state: AppGlobalStateEntity;
}

export const initialAppGlobalState: AppGlobalState = {
    state: new AppGlobalStateEntity()
}

export const appGlobalStateSlice = createSlice({
    name: 'appGlobalState',
    initialState: initialAppGlobalState,
    reducers: {
        setAppGlobalState: (state, action) => {
            state.state = action.payload;
        }
    }
});

export const {setAppGlobalState} = appGlobalStateSlice.actions;