import { Grid } from "@mui/material";
import FilterPanel from "./FilterPanel";
import TestsTable from "./TestsTable";

export default function StatisticsPage() {

    return (
        <Grid container>
            <Grid item xs={3}>
                <FilterPanel />
            </Grid>
            <Grid item xs={9}>
                <TestsTable />
            </Grid>
        </Grid>
    );
}