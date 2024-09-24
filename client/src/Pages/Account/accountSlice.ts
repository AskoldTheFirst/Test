import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { UserDto } from "../../Biz/DTOs/UserDto";
import { FieldValues } from "react-hook-form";
import agent from "../../Biz/agent";

export interface AccountState {
    user: UserDto | null;
}

const initialState: AccountState = {
    user: null
}

export const signInUser = createAsyncThunk<UserDto, FieldValues>(
    'account/signInUser',
    async (data, thunkAPI) => {
        try {
            const userDto = await agent.Account.login(data);
            localStorage.setItem('user', JSON.stringify(userDto));
            return userDto;
        } catch (error: any) {
            localStorage.removeItem('user');
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

export const fetchCurrentUser = createAsyncThunk<UserDto>(
    'account/fetchCurrentUser',
    async (_, thunkAPI) => {
        thunkAPI.dispatch(setUser(JSON.parse(localStorage.getItem('user')!)));
        try {
            const userDto = await agent.Account.currentUser();
            localStorage.setItem('user', JSON.stringify(userDto));
            return userDto;
        } catch (error: any) {
            localStorage.removeItem('user');
            return thunkAPI.rejectWithValue({ error: error.data })
        }
    },
    {
        condition: () => {
            if (!localStorage.getItem('user')) return false;
        }
    }
);

export const accountSlice = createSlice({
    name: 'account',
    initialState,
    reducers: {
        setUser: (state, action) => {
            state.user = action.payload;
        },
        signOut: (state) => {
            state.user = null;
            localStorage.removeItem('user');
            //router.navigate('/');
        },
    },
    extraReducers: (builder => {
        builder.addCase(signInUser.fulfilled, (state, action) => {
            state.user = action.payload;
        });
        builder.addCase(signInUser.rejected, (_state, action) => {
            console.log("signInUser.rejected" + action.payload);
        });

        builder.addCase(fetchCurrentUser.fulfilled, (state, action) => {
            state.user = action.payload;
        });
        builder.addCase(fetchCurrentUser.rejected, (_state, action) => {
            console.log("fetchCurrentUser.rejected" + action.payload);
        });
    })
});

export const { setUser, signOut } = accountSlice.actions;