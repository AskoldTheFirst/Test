import { Card, CardContent, Grid, Typography } from "@mui/material";

export default function StatisticsComponent() {
    return (
        <>
            <hr />
            <Grid container>
                <Card sx={{ width: 145 }}>
                    <CardContent>

                        <Typography>
                            Java Script
                        </Typography>

                    </CardContent>
                </Card>
                <Card sx={{ width: 145 }}>
                    <CardContent>

                        <Typography>
                            Java Script
                        </Typography>

                    </CardContent>
                </Card>
                <Card sx={{ width: 145 }}>
                    <CardContent>

                        <Typography>
                            Java Script
                        </Typography>

                    </CardContent>
                </Card>
            </Grid>
        </>
    )
}