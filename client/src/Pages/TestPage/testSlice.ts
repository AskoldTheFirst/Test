import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { CurrentTest } from "../../Biz/Entities/CurrentTest";
import agent from "../../Biz/agent";
import { InitTestResultDto } from "../../Biz/DTOs/InitTestResultDto";
import { router } from "../../App/Routes";
import { QuestionDto } from "../../Biz/DTOs/QuestionDto";
import { NextQuestionStateDto } from "../../Biz/DTOs/NextQuestionStateDto";
import { Helper } from "../../Biz/Helper";

export interface TestState {
    test: CurrentTest | null;
}

export const initialState: TestState = {
    test: null
}

export const initiateTest = createAsyncThunk<InitTestResultDto, string>(
    'test/initiateTest',
    async (data, thunkAPI) => {
        try {
            return await agent.Test.initiateNewTest(data);
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

export const nextQuestion = createAsyncThunk<QuestionDto, number>(
    'test/nextQuestion',
    async (data, thunkAPI) => {
        try {
            return await agent.Test.nextQuestion(data);
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

export const nextQuestionState = createAsyncThunk<NextQuestionStateDto, number | null>(
    'test/nextQuestionState',
    async (data, thunkAPI) => {
        try {
            return await agent.Test.nextQuestionState(data);
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

export const testSlice = createSlice({
    name: 'test',
    initialState,
    reducers: {
        initState: (state, action) => {
            state.test = action.payload;
        },
        incrementQuestionNumber: (state) => {
            if (state.test !== null) {

                const newState = {
                    questionNumber: state.test.questionNumber + 1,
                    question: state.test.question,
                    testId: state.test.testId,
                    totalAmount: state.test.totalAmount,
                    secondsLeft: state.test.secondsLeft,
                } as CurrentTest;

                state.test = newState;
            }
        },
    },
    extraReducers: (builder => {
        builder.addCase(initiateTest.fulfilled, (state, action) => {
            const newState = {
                questionNumber: 0,
                question: null,
                testId: action.payload.testId,
                totalAmount: action.payload.totalAmount,
                secondsLeft: action.payload.secondsLeft,
            } as CurrentTest;
            state.test = newState;
            localStorage.setItem(Helper.TestKey, newState.testId.toString());
            router.navigate('/test');
        });
        builder.addCase(initiateTest.rejected, (_state, action) => {
            localStorage.removeItem(Helper.TestKey);
            console.log("initiateTest.rejected" + action.payload);
        });

        builder.addCase(nextQuestion.fulfilled, (state, action) => {
            if (state.test !== undefined && state.test !== null) {
                const newState = {
                    questionNumber: state.test.questionNumber,
                    question: action.payload,
                    testId: state.test.testId,
                    totalAmount: state.test.totalAmount,
                    secondsLeft: state.test.secondsLeft,
                } as CurrentTest;
                state.test = newState;
            }
        });
        builder.addCase(nextQuestion.rejected, (_state, action) => {
            console.log("nextQuestion.rejected" + action.payload);
        });

        builder.addCase(nextQuestionState.fulfilled, (state, action) => {
            const newState = {
                questionNumber: action.payload.questionNumber,
                question: action.payload.question,
                testId: action.payload.question.testId,
                totalAmount: action.payload.totalAmount,
                secondsLeft: action.payload.secondsLeft,
            } as CurrentTest;
            state.test = newState;
            localStorage.setItem(Helper.TestKey, newState.testId.toString());
        });
        builder.addCase(nextQuestionState.rejected, (_state, action) => {
            localStorage.removeItem(Helper.TestKey);
            console.log("nextQuestionState.rejected" + action.payload);
        });
    })
});

export const { initState, incrementQuestionNumber } = testSlice.actions;