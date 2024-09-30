import { Paper, styled, Table, TableBody, TableCell, tableCellClasses, TableContainer, TableFooter, TableHead, TablePagination, TableRow, Typography } from "@mui/material";
import { useSelector } from "react-redux";
import { RootState } from "../../App/configureStore";
import { useEffect, useState } from "react";
import agent from "../../Biz/agent";
import { PageDto } from "../../Biz/DTOs/PageDto";
import { PageFilterParamsDto } from "../../Biz/DTOs/PageFilterParamsDto";
import { TestRowDto } from "../../Biz/DTOs/TestRowDto";
import { Helper } from "../../Biz/Helper";

const StyledTableCell = styled(TableCell)(({ theme }) => ({

    [`&.${tableCellClasses.head}`]: {
        backgroundColor: '#72906d',
        color: theme.palette.common.white,
    },
    [`&.${tableCellClasses.body}`]: {
        fontSize: 14,
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

export default function TestsTable() {
    const [pageNumber, setPageNumber] = useState<number>(1);
    const [pageSize, setPageSize] = useState<number>(10);
    const [totalAmount, setTotalAmount] = useState<number>(0);
    const [rows, setRows] = useState<TestRowDto[]>([]);
    const { filter } = useSelector((state: RootState) => state.filter);


    useEffect(() => {

        if (filter == null)
            return;

        const pageFilter = {
            pageNumber: pageNumber,
            pageSize: pageSize,
            userSearch: filter.userSearch,
            techIds: filter.techIds,
            period: filter.period
        } as PageFilterParamsDto;

        agent.Statistics.page(pageFilter)
            .then((pageDto: PageDto) => {
                setRows(pageDto.rows);
                setTotalAmount(pageDto.total);
            });

    }, [filter, pageNumber, pageSize]);

    const handleChangePage = (
        _event: React.MouseEvent<HTMLButtonElement> | null,
        newPage: number,
    ) => {
        setPageNumber(newPage + 1);
    };

    const handleChangeRowsPerPage = (
        event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
    ) => {

        setPageSize(parseInt(event.target.value, 10));
    };

    return (
        <>
            <Typography variant="h6">Test results:</Typography>
            <TableContainer component={Paper} sx={{marginTop: '8px'}}>
                <Table sx={{ minWidth: 500 }} aria-label="customized table">
                    <TableHead>
                        <TableRow>
                            <StyledTableCell align="left">Technology</StyledTableCell>
                            <StyledTableCell align="left">Username</StyledTableCell>
                            <StyledTableCell align="center">Score</StyledTableCell>
                            <StyledTableCell align="center">Time spent</StyledTableCell>
                            <StyledTableCell align="center">Date</StyledTableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {rows.map((row, index) => (
                            <StyledTableRow key={index}>
                                <StyledTableCell align="left">{row.techName}</StyledTableCell>
                                <StyledTableCell align="left">{row.username}</StyledTableCell>
                                <StyledTableCell align="center">{row.score}</StyledTableCell>
                                <StyledTableCell align="center">{Helper.FromSecondsToMinAndSeconds(row.timeSpentInSeconds)}</StyledTableCell>
                                <StyledTableCell align="center">{row.date}</StyledTableCell>
                            </StyledTableRow>
                        ))}
                    </TableBody>
                    <TableFooter>
                        <TableRow>
                            <TablePagination
                                defaultValue={10}
                                rowsPerPageOptions={[10, 25, 50]}
                                colSpan={4}
                                count={totalAmount}
                                rowsPerPage={pageSize}
                                showFirstButton={true}
                                showLastButton={true}
                                page={pageNumber - 1}
                                slotProps={{
                                    select: {
                                        inputProps: {
                                            'aria-label': 'rows per page',
                                        },
                                        native: true,
                                    },
                                }}
                                onRowsPerPageChange={handleChangeRowsPerPage}
                                onPageChange={handleChangePage}
                            />
                        </TableRow>
                    </TableFooter>
                </Table>
            </TableContainer>
        </>
    );
}