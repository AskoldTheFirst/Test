import { Button, Card, CardContent, Typography } from "@mui/material";
import { Link } from "react-router-dom";

interface Props {
    id?: number
    techName?: string;
    questionsAmount?: number;
    duration?: number;
    isEmpty?: boolean;
}

export default function TechnologyCardComp(params: Props) {
    return (
        <Card style={{ background: '#d8dde6', width: 200, height: 180 }}>
            {params.isEmpty ? (<></>) : (
                <>
                    <CardContent>
                        <Typography variant="h4" fontFamily='Montserrat' color='#4e4e81'>
                            {params.techName}
                        </Typography>
                        <Typography marginLeft={'60px'} textAlign={'left'} fontSize='8pt' fontFamily='Montserrat' color='#4e4e81'>{params.questionsAmount} questions</Typography>
                        <Typography marginLeft={'60px'} textAlign={'left'} fontSize='8pt' fontFamily='Montserrat' color='#4e4e81'>{params.duration} minutes</Typography>
                    </CardContent>
                    <Link to={`/commenceTest/${params.id}`}>
                        <Button variant="contained" sx={{ margin: '14px', background: '#60639b' }}>
                            Start Test
                        </Button>
                    </Link>
                </>
            )}
        </Card>
    );
}