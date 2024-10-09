import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { FormControl, FormControlLabel, FormLabel, Radio, RadioGroup, Typography } from "@mui/material";
import agent from "../../Biz/agent";
import { useDispatch, useSelector } from "react-redux";
import { AppDispatch, RootState } from "../../App/configureStore";
import { incrementQuestionNumber, nextQuestion, nextQuestionState } from "./testSlice";
import QuestionHeaderCmp from "./QuestionHeaderCmp";
import { LoadingButton } from "@mui/lab";

export default function TestPage() {
    const dispatch = useDispatch<AppDispatch>();
    const [answer, setAnswer] = useState<number>(0);
    const [flag, setFlag] = useState<boolean>(true);
    const [loading, setLoading] = useState<boolean>(false);
    const navigate = useNavigate();

    const { test } = useSelector((state: RootState) => state.test);

    useEffect(() => {
        setLoading(true);
        if (test === null) {
            const testId = localStorage.getItem('testId');
            dispatch(nextQuestionState(testId === null ? null : parseInt(testId)))
                .then(() => setLoading(false));
        }
        else {
            dispatch(nextQuestion(test.testId))
                .then(() => setLoading(false));
        }
    }, [flag]);

    if (test === null || test.question === null)
        return <></>;

    async function nextHandler() {
        if (test !== null) {
            await agent.Test.answer(test.testId, test.question?.questionId!, answer)
                .then(async () => {
                    if (test.questionNumber === test.totalAmount) {
                        await completeTestAsync(test.testId);
                    }
                    else {
                        dispatch(incrementQuestionNumber());
                        setFlag(!flag);
                    }
                    setAnswer(0);
                });
        }
    }

    function answerChangeHandler(ev: any) {
        setAnswer(parseInt(ev.target.value));
    }

    async function completeTestAsync(testId: number) {
        await agent.Test.complete(testId)
            .then(() => navigate('/test-result'));
    }

    return (
        <>
            <QuestionHeaderCmp
                number={test.questionNumber}
                total={test.totalAmount}
                timeLeftInSeconds={test.secondsLeft}
                completeHandler={async () => await completeTestAsync(test.testId)}
            />
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
                    value={answer}
                >
                    <FormControlLabel sx={{ marginTop: '6px' }} value="1" control={<Radio />} label={test.question.answer1} />
                    <FormControlLabel value="2" control={<Radio />} label={test.question.answer2} />
                    <FormControlLabel value="3" control={<Radio />} label={test.question.answer3} />
                    <FormControlLabel value="4" control={<Radio />} label={test.question.answer4} />
                </RadioGroup>
            </FormControl>
            <hr />
            <br />

            <LoadingButton variant="contained" loading={loading} disabled={answer === 0} onClick={nextHandler}>Next</LoadingButton>
        </>
    );
}