import { Grid, Stack, Typography } from "@mui/material";
import { useEffect, useState } from "react";

interface Props {
    number: number;
    total: number;
    timeLeftInSeconds: number;
    completeHandler: () => void;
}

export default function QuestionHeaderCmp({ number, total, timeLeftInSeconds, completeHandler }: Props) {
    const [leftSeconds, setLeftSeconds] = useState<number>(0);
    const [leftMinutes, setLeftMinutes] = useState<number>(0);
    const [leftTotal, setLeftTotal] = useState<number>(timeLeftInSeconds);

    useEffect(() => {
        
        const leftSec = leftTotal % 60;
        const leftMin = (leftTotal - leftSec) / 60;
        setLeftSeconds(leftSec);
        setLeftMinutes(leftMin);

        const id = setInterval(timerHandler, 1000);
        return () => clearInterval(id);
    }, [leftTotal]);

    function timerHandler() {
        const left = leftTotal - 1;
        setLeftTotal(left);

        if (left < 1) {
            completeHandler();
            return;
        }

        const leftSec = left % 60;
        const leftMin = (left - leftSec) / 60;

        setLeftSeconds(leftSec);
        setLeftMinutes(leftMin);
    }

    if (leftTotal < 1 || (leftMinutes < 1 && leftSeconds < 1))
        return <></>;

    return (
        <Grid container>
            <Grid item xl={4} lg={4} md={4} sm={4} xs={4}><Typography fontSize={16} variant="h6">Question {number} of {total}</Typography></Grid>
            <Grid item xl={8} lg={8} md={8} sm={8} xs={8} sx={{ display: 'flex', justifyContent: 'end' }}>
                <Stack direction='row' sx={{ textAlign: 'end', marginRight: '24px' }}>
                    <Typography fontSize={14} variant="h6">Time left:</Typography>
                    <Typography fontSize={14} variant="h6" sx={{ width: '32px', textAlign: 'right' }}>{leftMinutes}m</Typography>
                    <Typography fontSize={14} variant="h6" sx={{ width: '26px', textAlign: 'right' }}>{leftSeconds}s</Typography>
                </Stack>
            </Grid>
        </Grid>
    );
}