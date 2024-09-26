import { Button, Typography } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { useEffect, useState } from "react";
import { TechnologyDto } from "../../Biz/DTOs/TechnologyDto";
import { AppDispatch, RootState } from "../../App/configureStore";
import { initiateTest } from "./testSlice";

export default function TestCommencePage() {
    const dispatch = useDispatch<AppDispatch>();
    const { testId } = useParams<string>();
    const { technologies } = useSelector((state: RootState) => state.tech);
    const [currentTechnology, setCurrentTechnology] = useState<TechnologyDto>();
    const navigate = useNavigate();

    useEffect(() => {

        if (testId === undefined) {
            navigate('/');
            return;
        }

        if (technologies.length > 0)
            setCurrentTechnology(figureOutTechnology(parseInt(testId), technologies));

    }, [technologies]);

    function figureOutTechnology(id: number, technologies: TechnologyDto[]) {
        for (let i = 0; i < technologies.length; ++i)
            if (technologies[i].id === id)
                return technologies[i];

        throw "technology was not found";
    }

    function StartHandler() {
        if (currentTechnology === undefined)
            return;

        dispatch(initiateTest(encodeURIComponent(currentTechnology.name)));
    }

    return (
        <center style={{ marginTop: '150px' }}>
            {
                currentTechnology !== undefined ?
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