import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { AppDispatch, RootState } from "../../App/configureStore";
import { getTopsAsync } from "../../App/statisticsSlice";
import { Grid } from "@mui/material";
import TopCardComp from "./TopCardComp";

export default function StatisticsPage() {

    const dispatch = useDispatch<AppDispatch>();
    const { tops } = useSelector((state: RootState) => state.tops);

    useEffect(() => {
        dispatch(getTopsAsync(10));
    }, []);

    return (
        <>
            <Grid container>
                {tops.map((t) => (
                    t.lines.length != 0 ?
                        (<Grid item key={t.techName}>
                            <TopCardComp techName={t.techName} topLines={t.lines} />
                        </Grid>) : ('')
                ))}
            </Grid>
        </>
    );
}