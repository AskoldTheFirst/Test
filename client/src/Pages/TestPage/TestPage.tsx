import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button, FormControl, FormControlLabel, FormLabel, Radio, RadioGroup, Typography } from "@mui/material";
import { QuestionDto } from "../../Biz/DTOs/QuestionDto";
import agent from "../../Biz/agent";
import { useSelector } from "react-redux";
import { RootState } from "@reduxjs/toolkit/query";

export default function TestPage() {
    const [currentQuestion, setCurrentQuestion] = useState<QuestionDto | undefined>();
    const [currentQuestionNumber, setCurrentQuestionNumber] = useState<number>(1);
    const [answer, setAnswer] = useState<number>(0);
    const navigate = useNavigate();
    const { state } = useSelector((state: RootState) => state.globalState);

    useEffect(() => {
        console.log(state);
        if (currentQuestionNumber == state.currentTest.amount) {
            agent.Test.complete(state.testId)
                .then(() => navigate(`/test-result`));
        }
        else {
            agent.Test.nextQuestion(state.testId)
                .then(q => setCurrentQuestion(q));
        }
    }, [currentQuestionNumber]);

    if (currentQuestion == undefined)
        return <></>;

    function nextHandler() {
        if (answer != 0) {
            agent.Test.answer(state.testId, currentQuestion?.questionId!, answer)
            setCurrentQuestionNumber(currentQuestionNumber + 1);
        }
    }

    function answerChangeHandler(ev: any) {
        setAnswer(parseInt(ev.target.value));
    }

    return (
        <>
            <Typography fontSize={16} variant="h6">Question {currentQuestionNumber} of {state.currentTest.amount}</Typography>
            <br />
            <Typography fontSize={20} variant="h6">{currentQuestion.text}</Typography>
            <br />

            <FormControl>
                <FormLabel id="demo-radio-buttons-group-label"><Typography fontSize={16} color={"gray"}>Possible answers:</Typography></FormLabel>
                <RadioGroup
                    aria-labelledby="demo-radio-buttons-group-label"
                    defaultValue="0"
                    name="radio-buttons-group"
                    onChange={answerChangeHandler}
                >
                    <FormControlLabel sx={{marginTop: '6px'}} value="1" control={<Radio />} label={currentQuestion.answer1} />
                    <FormControlLabel value="2" control={<Radio />} label={currentQuestion.answer2} />
                    <FormControlLabel value="3" control={<Radio />} label={currentQuestion.answer3} />
                    <FormControlLabel value="4" control={<Radio />} label={currentQuestion.answer4} />
                </RadioGroup>
            </FormControl>
            <hr />
            <br />

            <Button variant="contained" disabled={answer == 0} onClick={nextHandler}>Next</Button>
        </>
    );
}