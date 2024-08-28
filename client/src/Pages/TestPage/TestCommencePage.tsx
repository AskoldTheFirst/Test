import { Button, Typography } from "@mui/material";
import { useParams } from "react-router-dom";
import { useSelector } from "react-redux";
import { RootState } from "@reduxjs/toolkit/query";
import { useState } from "react";
import { TechnologyDto } from "../../Biz/DTOs/TechnologyDto";

export default function TestCommencePage() {
    const { id } = useParams<string>();
    const { technologies } = useSelector((state: RootState) => state.tech);
    const [currentTechnology, setCurrentTechnology] = useState<TechnologyDto>(figureOutTechnology(id, technologies));

    function figureOutTechnology(idParam: string | undefined, technologies: TechnologyDto[]) {
        if (idParam === undefined)
            throw "technology was not found";

        const idNumber = parseInt(idParam);

        for(let i = 0; i < technologies.length; ++i)
            if (technologies[i].id == idNumber)
                return technologies[i];

        throw "technology was not found";
    }

    return (
        <>
            <Typography variant="h4">You are about to start {currentTechnology.name}</Typography>
            <Typography variant="h6">Amount of question: {currentTechnology.amount}</Typography>
            <Typography variant="h6">Time limit is {currentTechnology.duration} minutes.</Typography>

            <Button variant="contained">Back</Button>
            <Button variant="contained">Start</Button>
        </>
    );
}