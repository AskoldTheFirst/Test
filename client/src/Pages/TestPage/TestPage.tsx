import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button, FormControl, FormControlLabel, FormLabel, Radio, RadioGroup, Typography } from "@mui/material";
import agent from "../../Biz/agent";
import { useDispatch, useSelector } from "react-redux";
import { AppDispatch, RootState } from "../../App/configureStore";
import { incrementQuestionNumber, nextQuestion, nextQuestionState } from "./testSlice";

export default function TestPage() {
    const dispatch = useDispatch<AppDispatch>();
    const [answer, setAnswer] = useState<number>(0);
    const [flag, setFlag] = useState<boolean>(true);
    const navigate = useNavigate();

    const { test } = useSelector((state: RootState) => state.test);

    useEffect(() => {
        if (test === null) {
            const testId = localStorage.getItem('testId');
            dispatch(nextQuestionState(testId === null ? null : parseInt(testId)));
        }
        else {
            dispatch(nextQuestion(test.testId));
        }
    }, [flag]);

    if (test === null || test.question === null)
        return <></>;

    async function nextHandler() {
        if (test !== null) {
            await agent.Test.answer(test.testId, test.question?.questionId!, answer)
                .then(async () => {
                    if (test.questionNumber === test.totalAmount) {
                        await agent.Test.complete(test.testId)
                            .then(() => navigate('/test-result'));

                    }
                    else {
                        dispatch(incrementQuestionNumber());
                        setFlag(!flag);
                    }
                });
        }
    }

    function answerChangeHandler(ev: any) {
        setAnswer(parseInt(ev.target.value));
    }

    return (
        <>
            <Typography fontSize={16} variant="h6">Question {test.questionNumber} of {test.totalAmount}</Typography>
            <br />
            <Typography fontSize={20} variant="h6">{test.question.text}</Typography>
            <br />

            <FormControl>
                <FormLabel id="demo-radio-buttons-group-label"><Typography fontSize={16} color={"gray"}>Possible answers:</Typography></FormLabel>
                <RadioGroup
                    aria-labelledby="demo-radio-buttons-group-label"
                    defaultValue="0"
                    name="radio-buttons-group"
                    onChange={answerChangeHandler}
                >
                    <FormControlLabel sx={{ marginTop: '6px' }} value="1" control={<Radio />} label={test.question.answer1} />
                    <FormControlLabel value="2" control={<Radio />} label={test.question.answer2} />
                    <FormControlLabel value="3" control={<Radio />} label={test.question.answer3} />
                    <FormControlLabel value="4" control={<Radio />} label={test.question.answer4} />
                </RadioGroup>
            </FormControl>
            <hr />
            <br />

            <Button variant="contained" disabled={answer == 0} onClick={nextHandler}>Next</Button>
        </>
    );
}