import { useEffect, useState } from "react";
import agent from "../../Biz/agent";
import { TestResultDto } from "../../Biz/DTOs/TestResultDto";
import { Typography } from "@mui/material";
import { useSelector } from "react-redux";
import { RootState } from "../../App/configureStore";

export default function TestResultPage() {
    const [result, setResult] = useState<TestResultDto>();
    const { test } = useSelector((state: RootState) => state.test);

    useEffect(() => {
        if (test !== null)
            agent.Test.result(test?.testId).then(result => setResult(result));
    }, []);

    if (test === null)
        return <></>

    return (
        <center>
            <Typography>Congratulate!</Typography>
            <Typography>You have passed through the {test.technologyName} test</Typography>
            <Typography>with the score {result?.score}.</Typography>
        </center>
    );
}