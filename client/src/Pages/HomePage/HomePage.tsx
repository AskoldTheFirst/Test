import TechnologyComponent from "./TechnologyComponent";
import StatisticsComponent from "./StatisticsPreviewComponent";
import { Box } from "@mui/material";

export default function HomePage() {
    return (
        <>
            <Box sx={{marginTop: '30px'}}>
                <TechnologyComponent />
                <Box sx={{marginTop: '20px'}} />
                <StatisticsComponent />
            </Box>
        </>
    )
}