import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { UserDto } from "../../Biz/DTOs/UserDto";
import { FieldValues } from "react-hook-form";
import agent from "../../Biz/agent";
import { router } from "../../App/Routes";
import { Helper } from "../../Biz/Helper";

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
            localStorage.setItem(Helper.UserKey, JSON.stringify(userDto));
            return userDto;
        } catch (error: any) {
            localStorage.removeItem(Helper.UserKey);
            return thunkAPI.rejectWithValue({ error: error.status });
        }
    }
);

export const fetchCurrentUser = createAsyncThunk<UserDto>(
    'account/fetchCurrentUser',
    async (_, thunkAPI) => {
        thunkAPI.dispatch(setUser(JSON.parse(localStorage.getItem(Helper.UserKey)!)));
        try {
            const userDto = await agent.Account.currentUser();
            localStorage.setItem(Helper.UserKey, JSON.stringify(userDto));
            return userDto;
        } catch (error: any) {
            signOut();
            return thunkAPI.rejectWithValue({ error: error.data })
        }
    },
    {
        condition: () => {
            if (!localStorage.getItem(Helper.UserKey)) return false;
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
            localStorage.removeItem(Helper.UserKey);
            router.navigate('/login');
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