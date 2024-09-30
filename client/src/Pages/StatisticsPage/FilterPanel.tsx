import { Box, Typography, FormControl, FormLabel, TextField, Select, MenuItem, Grid, FormControlLabel, Checkbox } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import { AppDispatch, RootState } from "../../App/configureStore";
import { setIds, setUserSearch, setPeriod } from "./filterSlice";


export default function FilterPanel() {
    const { technologies } = useSelector((state: RootState) => state.tech);
    const { ids, filter } = useSelector((state: RootState) => state.filter);
    const dispatch = useDispatch<AppDispatch>();

    function onChangeHandler(param: any) {
        const idsCopy = [...ids];
        const clickedId: number = parseInt(param.target.value);

        if (param.target.checked) {
            idsCopy.push(clickedId);
        }
        else {
            const index = idsCopy.indexOf(clickedId);
            idsCopy.splice(index, 1);
        }

        dispatch(setIds(idsCopy));
    }

    function isChecked(id: number): boolean {
        return ids.indexOf(id) !== -1;
    }

    return (
        <Box sx={{ marginRight: 6, minWidth: 120 }}>

            <Typography variant="h6">
                Filters:
            </Typography>
            <hr />

            <Grid container>
                {technologies.map((t) => (
                    <Grid item xs={6} sx={{ textAlign: 'center', marginBottom: '6px', marginTop: '6px' }}>
                        <FormControlLabel
                            value={t.id}
                            control={<Checkbox checked={isChecked(t.id)} onChange={onChangeHandler} />}
                            label={t.name}
                            labelPlacement="top"
                        />
                    </Grid>
                ))}
            </Grid>

            <FormControl fullWidth sx={{ marginTop: 4 }}>
                <FormLabel htmlFor="searchInUsers">Search in users:</FormLabel>
                <TextField
                    id="searchInUsers"
                    variant="outlined"
                    fullWidth
                    size="small"
                    value={filter.userSearch}
                    onChange={(event: any) => {
                        dispatch(setUserSearch(event.target.value));
                    }}
                />
            </FormControl>

            <FormControl fullWidth sx={{ marginTop: 2 }}>
                <FormLabel htmlFor="period">Time periods:</FormLabel>
                <Select
                    id="period"
                    value={filter.period}
                    size="small"
                    onChange={(event: any) => dispatch(setPeriod(event.target.value))}
                >
                    <MenuItem value={0}>All periods</MenuItem>
                    <MenuItem value={1}>Last 24 hours</MenuItem>
                    <MenuItem value={2}>Last 7 days</MenuItem>
                    <MenuItem value={3}>Last 30 days</MenuItem>
                </Select>
            </FormControl>

        </Box>
    );
}