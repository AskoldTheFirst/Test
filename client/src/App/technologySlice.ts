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
            console.log("NNN2");
            return await agent.App.technologies();
        }
        catch (error: any) {
            console.log("VVV2");
            return thunkAPI.rejectWithValue({error: error.data});
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
            console.log('tech - get -pending');
        });
        builder.addCase(getTechnologiesAsync.fulfilled, (state, action) => {
            console.log(action.payload);
            state.technologies = action.payload;
            console.log('fulfilled');
        });
        builder.addCase(getTechnologiesAsync.rejected, (_, action) => {
            console.log('rejected' + action.payload);
        });
    })
});

export const {setTechnology} = technologySlice.actions;