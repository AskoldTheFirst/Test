import { Grid, useMediaQuery, useTheme } from "@mui/material";
import { RootState } from "../../../App/configureStore";
import { useSelector } from "react-redux";
import { TechnologyDto } from "../../../Biz/DTOs/TechnologyDto";
import TechnologyCardComp from "./TechnologyCardComp";
import EmptyComp from "./EmptyComp";

export default function TechnologyComp() {
    const { technologies } = useSelector((state: RootState) => state.tech);

    const theme = useTheme();
    const matchesSM = useMediaQuery(theme.breakpoints.only('sm'));
    const matchesXS = useMediaQuery(theme.breakpoints.only('xs'));
    const matchesMD = useMediaQuery(theme.breakpoints.only('md'));

    function GenerateGridItemsArray(technologies: TechnologyDto[]): Array<TechnologyDto | null> {

        if (technologies == null || technologies.length == 0)
            return [];

        const buttonAmount = technologies.length;
        let array = new Array<TechnologyDto | null>(0);

        if (matchesSM || matchesXS) {
            for (let i = 0; i < technologies.length; ++i) {
                array.push(technologies[i]);
            }
            return array;
        }

        let currentAmount = 0;
        let currentButtonsAmount = 0;
        const randBount = matchesMD ? 2 : 3;
        const lineCellsAmount = matchesMD ? 4 : 5

        do {
            let amountOfEmptyElements = getRndInteger(0, randBount)
            console.log('### ' + amountOfEmptyElements);
            currentAmount += amountOfEmptyElements;
            for (; amountOfEmptyElements > 0; --amountOfEmptyElements) {
                array.push(null);
            }

            array.push(technologies[currentButtonsAmount++]);
            ++currentAmount;
        } while (currentButtonsAmount < buttonAmount)
        
        while (currentAmount++ % lineCellsAmount != 0) {
            array.push(null);
        }

        return array;
    }

    function getRndInteger(min: number, max: number) {
        return Math.floor(Math.random() * (max - min)) + min + 1;
    }

    if (matchesMD) {
        return (
            <Grid container spacing={2} columns={16}>
                {GenerateGridItemsArray(technologies)?.map((n) => (
                    <Grid item xl={3} lg={3} md={4} sm={0} xs={0}>
                        <center>
                            <TechnologyCardComp techName={n?.name} questionsAmount={n?.amount} duration={n?.duration} isEmpty={n == null} />
                        </center>
                    </Grid>
                ))}
            </Grid >
        );
    }
    else {
        return (
            <Grid container spacing={2} columns={16}>
                {GenerateGridItemsArray(technologies)?.map((n) => (
                    n == null ?
                        (<Grid item xl={3} lg={3} md={4} sm={0} xs={0}>
                            <center>
                                <EmptyComp />
                            </center>
                        </Grid>) :
                        (<Grid item xl={3} lg={3} md={4} sm={8} xs={8}>
                            <center>
                                <TechnologyCardComp techName={n.name} questionsAmount={n.amount} duration={n.duration} />
                            </center>
                        </Grid>
                        )
                ))}
            </Grid >
        );
    }
}