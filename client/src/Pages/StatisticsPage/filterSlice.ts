import { createSlice } from "@reduxjs/toolkit";
import { Filter } from "../../Biz/Entities/Filter";
import { Helper } from "../../Biz/Helper";

export interface FilterState {
    filter: Filter;
    ids: number[];
}

export const initialState: FilterState = {
    filter: {
        period: 0,
        techIds: '',
        userSearch: ''
    } as Filter,
    ids: []
}

export const filterSlice = createSlice({
    name: 'statistics',
    initialState,
    reducers: {
        setFilter: (state, action) => {
            state.filter = action.payload;
        },
        setIds: (state, action) => {
            state.ids = action.payload;
            state.filter = {
                period: state.filter.period,
                userSearch: state.filter.userSearch,
                techIds: Helper.ConvertArrayToString(action.payload)
            } as Filter;
        },
        setUserSearch: (state, action) => {
            state.filter = {
                period: state.filter.period,
                userSearch: action.payload,
                techIds: state.filter.techIds,
            } as Filter;
        },
        setPeriod: (state, action) => {
            state.filter = {
                period: action.payload,
                userSearch: state.filter.userSearch,
                techIds: state.filter.techIds
            } as Filter;
        },
    }
});

export const { setFilter, setIds, setUserSearch, setPeriod } = filterSlice.actions;