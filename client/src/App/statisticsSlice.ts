import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { TopDto } from "../Biz/DTOs/TopDto";
import agent from "../Biz/agent";

export interface Statistics {
    tops: TopDto[];
}

export const initialStatistics: Statistics = {
    tops: new Array<TopDto>()
}

export const getTopsAsync = createAsyncThunk<TopDto[], number>(
    'statistics/getTopsAsync',
    async (amount, thunkAPI) => {
        try {
            return await agent.App.tops(amount);
        }
        catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

export const statisticsSlice = createSlice({
    name: 'statistics',
    initialState: initialStatistics,
    reducers: {
        setStatistics: (state, action) => {
            state.tops = action.payload;
        }
    },
    extraReducers: (builder => {
        builder.addCase(getTopsAsync.pending, () => {
            console.log('getTopsAsync.pending');
        });
        builder.addCase(getTopsAsync.fulfilled, (state, action) => {
            console.log(action.payload);
            state.tops = action.payload;
            console.log('getTopsAsync.fulfilled');
        });
        builder.addCase(getTopsAsync.rejected, (_, action) => {
            console.log('getTopsAsync.rejected' + action.payload);
        });
    })
});

export const { setStatistics } = statisticsSlice.actions;