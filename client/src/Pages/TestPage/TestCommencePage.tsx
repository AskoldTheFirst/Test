import { Box, Container, Typography } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { useEffect, useState } from "react";
import { TechnologyDto } from "../../Biz/DTOs/TechnologyDto";
import { AppDispatch, RootState } from "../../App/configureStore";
import { initiateTest } from "./testSlice";
import { LoadingButton } from "@mui/lab";

export default function TestCommencePage() {
    const dispatch = useDispatch<AppDispatch>();
    const { testId } = useParams<string>();
    const { technologies } = useSelector((state: RootState) => state.tech);
    const [currentTechnology, setCurrentTechnology] = useState<TechnologyDto>();
    const [loading, setLoading] = useState<boolean>(false);
    const navigate = useNavigate();

    useEffect(() => {

        if (testId === undefined) {
            window.alert('test');
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

        setLoading(true);
        dispatch(initiateTest(encodeURIComponent(currentTechnology.name))).then(() => setLoading(false));
    }

    if (currentTechnology === undefined)
        return <></>;

    return (
        <Container sx={{ display: 'flex', justifyContent: 'center' }}>
            <Box sx={{ width: '460px', marginTop: 8, textAlign: 'center' }}>
                <Typography fontSize={22} variant="h6">You are about to start {currentTechnology.name} test.</Typography>
                <Typography sx={{ marginTop: '18px' }} fontSize={16} variant="h6">You will have to answer {currentTechnology.amount} questions for {currentTechnology.duration} minutes.</Typography>
                <LoadingButton loading={loading} sx={{ marginTop: 4 }} variant="contained" disabled={currentTechnology == undefined} onClick={StartHandler}>Start</LoadingButton>
            </Box>
        </Container>
    );
}