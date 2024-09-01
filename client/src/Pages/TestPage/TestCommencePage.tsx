import { Button, Typography } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "@reduxjs/toolkit/query";
import { useEffect, useState } from "react";
import { TechnologyDto } from "../../Biz/DTOs/TechnologyDto";
import { AppDispatch } from "./configureStore";
import agent from "../../Biz/agent";
import { setGlobalState } from "../../App/globalStateSlice";

export default function TestCommencePage() {
    const dispatch = useDispatch<AppDispatch>();
    const { testId } = useParams<string>();
    const { technologies } = useSelector((state: RootState) => state.tech);
    const [currentTechnology, setCurrentTechnology] = useState<TechnologyDto>();
    const navigate = useNavigate();

    useEffect(() => {
        setCurrentTechnology(figureOutTechnology(testId, technologies));
    }, [testId, technologies]);

    function figureOutTechnology(idParam: string | undefined, technologies: TechnologyDto[]) {
        if (idParam === undefined)
            throw "technology was not found";

        const idNumber = parseInt(idParam);

        for (let i = 0; i < technologies.length; ++i)
            if (technologies[i].id == idNumber)
                return technologies[i];

        throw "technology was not found";
    }

    function StartHandler() {
        if (currentTechnology == undefined)
            return;

        agent.Test.initiateNewTest(encodeURIComponent(currentTechnology.name))
            .then(testId => {
                dispatch(setGlobalState({
                    currentTest: currentTechnology,
                    testId: testId
                }));
                navigate("/test");
            });
    }

    return (
        <center style={{ marginTop: '150px' }}>
            {
                currentTechnology != undefined ?
                    <>
                        <Typography fontSize={22} variant="h6">You are about to start {currentTechnology.name} test.</Typography>
                        <br />
                        <Typography fontSize={16} variant="h6">You will have to answer {currentTechnology.amount} questions.</Typography>
                        <Typography fontSize={16} variant="h6">And you will have just {currentTechnology.duration} seconds for each question.</Typography>

                        <Button sx={{ marginTop: 4 }} variant="contained" disabled={currentTechnology == undefined} onClick={StartHandler}>Start</Button>
                    </>
                    :
                    <>
                    </>
            }
        </center>
    );
}