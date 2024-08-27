import { Box, Button, Card, CardContent, Paper, styled, Table, TableBody, TableCell, tableCellClasses, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { TopLineDto } from "../../Biz/DTOs/TopLineDto";

interface Props {
    techName: string;
    topLines: TopLineDto[];
}

const StyledTableCell = styled(TableCell)(({ theme }) => ({
    [`&.${tableCellClasses.head}`]: {
        backgroundColor: theme.palette.primary.light,
        color: theme.palette.common.white,
        fontSize: 13,
    },
    [`&.${tableCellClasses.body}`]: {
        fontSize: 12,
    },
}));

const StyledTableRow = styled(TableRow)(({ theme }) => ({
    '&:nth-of-type(odd)': {
        backgroundColor: theme.palette.action.hover,
    },
    // hide last border
    '&:last-child td, &:last-child th': {
        border: 0,
    },
}));

export default function TopCardComp({ techName, topLines }: Props) {
    return (
        <Card sx={{ minWidth: 300, margin: '20px' }}>
            <CardContent>
                <Typography variant="h6" sx={{ textAlign: 'center' }}>
                    {techName}
                </Typography>
                <TableContainer component={Paper} sx={{marginTop: '10px'}}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <StyledTableCell align="left">Name</StyledTableCell>
                                <StyledTableCell align="right">Score</StyledTableCell>
                                <StyledTableCell align="right">Date</StyledTableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {topLines.map((line) => (
                                <StyledTableRow key={line.login}>
                                    <StyledTableCell component='th'>{line.login}</StyledTableCell>
                                    <StyledTableCell scope="row">{line.score}</StyledTableCell>
                                    <StyledTableCell scope="row">{line.date}</StyledTableCell>
                                </StyledTableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            </CardContent>
            <Box textAlign='center' sx={{marginBottom: '8px'}}>
                <Button size="small">View More</Button>
            </Box>
        </Card>
    );
}