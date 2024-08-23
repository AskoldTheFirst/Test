import { Button, Grid } from "@mui/material";
import { useEffect, useState } from "react";
import { RootState } from "../../App/configureStore";
import { useSelector } from "react-redux";
import { Technology } from "../../Biz/DTOs/Technology";

export default function TechnologyComponent() {
    const {technologies} = useSelector((state: RootState) => state.tech);
    const [gridItems, setGridItems] = useState<Array<string> | null>(null);

    useEffect(() => {
        setGridItems(GenerateGridItemsArray(technologies));
    }, [technologies]);

    function GenerateGridItemsArray(technologies: Technology[]): Array<string> {

        if (technologies == null || technologies.length == 0)
            return new Array<string>(0);

        const buttonAmount = technologies.length;
        let array = new Array<string>(0);
        const totalAmount = 16 * 4;
        let currentAmount = 0;
        let currentButtonsAmount = 0;
        const minRandBound = 2;

        let maxRandBound = Math.floor(totalAmount / (buttonAmount + buttonAmount * 0.7));
        if (maxRandBound > 6)
            maxRandBound = 6;

        if (maxRandBound < 2)
            maxRandBound = 2;

        console.log(`${totalAmount} ${maxRandBound} ${buttonAmount}`);

        do {
            let amountOfEmptyElements = getRndInteger(minRandBound, maxRandBound + 1);
            currentAmount += amountOfEmptyElements;
            console.log(`AAA: ${amountOfEmptyElements}`);
            for (; amountOfEmptyElements > -1; --amountOfEmptyElements) {
                array.push('');
            }

            array.push(technologies[currentButtonsAmount++].name);
            currentAmount += 3;
            console.log(`BBB: ${currentAmount}`);
        } while (currentButtonsAmount < buttonAmount)

        return array;
    }

    function getRndInteger(min: number, max: number) {
        return Math.floor(Math.random() * (max - min)) + min - 1;
    }

    return (
        <>
            <Grid container spacing={2} columns={16}>
                {gridItems?.map((n) => (
                    n == '' ?
                        (<Grid item xl={1} lg={1} md={1.25} sm={1.8} xs={0}>
                            <div className="testClass"></div>
                        </Grid>) :
                        (<Grid item xl={3} lg={3} md={5} sm={7} xs={11}>
                            <center>
                                <Button sx={{ width: 140, height: 38, color: 'white', background: 'grey', marginTop: '11px' }}>{n}</Button>
                            </center>
                        </Grid>
                        )
                ))}
            </Grid >
        </>
    );
}