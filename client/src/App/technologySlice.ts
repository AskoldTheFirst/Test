import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import agent from "../Biz/agent";
import { TechnologyDto } from "../Biz/DTOs/TechnologyDto";

export interface TechState {
    technologies: TechnologyDto[];
}

export const initialTechState: TechState = {
    technologies: new Array<TechnologyDto>()
}

export const getTechnologiesAsync = createAsyncThunk<TechnologyDto[]>(
    'technology/getTechnologiesAsync',
    async (_, thunkAPI) => {
        try {
            return await agent.App.technologies();
        }
        catch (error: any) {
            console.log(error.data);
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

export const technologySlice = createSlice({
    name: 'appState',
    initialState: initialTechState,
    reducers: {
        setTechnology: (state, action) => {
            state.technologies = action.payload;
        }
    },
    extraReducers: (builder => {
        builder.addCase(getTechnologiesAsync.pending, () => {
        });
        builder.addCase(getTechnologiesAsync.fulfilled, (state, action) => {
            console.log(action.payload);
            state.technologies = action.payload;
        });
        builder.addCase(getTechnologiesAsync.rejected, (_, action) => {
            console.log('rejected' + action.payload);
        });
    })
});

export const { setTechnology } = technologySlice.actions;