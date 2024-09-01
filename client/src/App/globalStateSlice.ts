import { createSlice } from "@reduxjs/toolkit";
import { GlobalStateEntity } from "../Biz/Entities/AppGlobalStateEntity";

export interface GlobalState {
    state: GlobalStateEntity | null;
}

export const initialGlobalState: GlobalState = {
    state: null
}

export const globalStateSlice = createSlice({
    name: 'globalState',
    initialState: initialGlobalState,
    reducers: {
        setGlobalState: (state, action) => {
            state.state = action.payload;
        }
    }
});

export const {setGlobalState} = globalStateSlice.actions;