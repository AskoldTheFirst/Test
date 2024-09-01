import { useEffect, useState } from "react";
import agent from "../../Biz/agent";
import { TestResultDto } from "../../Biz/DTOs/TestResultDto";
import { Typography } from "@mui/material";
import { useSelector } from "react-redux";
import { RootState } from "@reduxjs/toolkit/query";

export default function TestResultPage() {
    const [result, setResult] = useState<TestResultDto>();
    const { state } = useSelector((state: RootState) => state.globalState);

    useEffect(() => {
        agent.Test.result(state.testId).then(r => setResult(r));
    }, []);

    return (
        <center>
            <Typography>Congratulate!</Typography>
            <Typography>You have passed through the {state.currentTest.name} test</Typography>
            <Typography>with the score {result?.score}.</Typography>
        </center>
    );
}