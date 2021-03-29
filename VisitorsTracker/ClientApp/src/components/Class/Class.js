import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TablePagination from '@material-ui/core/TablePagination';
import TableRow from '@material-ui/core/TableRow';
import Box from '@material-ui/core/Box';

const columns = [
    { id: 'id', label: 'Номер', minWidth: 17 },
    { id: 'name', label: 'ПІБ', minWidth: 300 },
    { id: 'email', label: 'Пошта', minWidth: 250 },
];

function createData(id, name, email) {
    return { id, name, email };
}

const rows = [
    createData(1, 'Мельничук Станіслав', 'wasif@email.com'),
    createData(2, 'Орелецький Валентин', 'ali@email.com'),
    createData(3, 'Продан Анатолій', 'saad@email.com'),
    createData(4, 'Роєк Анастасія', 'asad@email.com'),
    createData(5, 'Стрільчук Вадим', 'asad@email.com'),
    createData(6, 'Романовський Михайло', 'asad@email.com'),
    createData(7, 'Щур Олександр', 'saad@email.com'),
    createData(8, 'Чебан Владислав', 'ali@email.com'),
    createData(9, 'Тихович Михайло', 'wasif@email.com'),
    createData(10, 'Тарица Олександр', 'wasif@email.com'),
    createData(11, 'Сілімір Руслан', 'wasif@email.com'),
    createData(12, 'Беженар Олександр', 'wasif@email.com'),
    createData(13, 'Буйновський Віктор', 'wasif@email.com'),
];

const useStyles = makeStyles({
    root: {
        width: '100%',
    },
    container: {
        maxHeight: 740,
    },
});

export default function StickyHeadTable() {
    const classes = useStyles();
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(10);

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(+event.target.value);
        setPage(0);
    };

    return (
        <Paper className={classes.root}>
            <Box textAlign="center"
                m={1}
                fontSize={30}
            >
                Список студентів групи
            </Box>
            <TableContainer className={classes.container}>
                <Table stickyHeader aria-label="sticky table">
                    <TableHead>
                        <TableRow>
                            {columns.map((column) => (
                                <TableCell
                                    key={column.id}
                                    style={{ minWidth: column.minWidth }}
                                >
                                    {column.label}
                                </TableCell>
                            ))}
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {rows.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((row) => {
                            return (
                                <TableRow hover role="checkbox" tabIndex={-1}>
                                    {columns.map((column) => {
                                        const value = row[column.id];
                                        return (
                                            <TableCell key={column.id}>
                                                {column.format && typeof value === 'number' ? column.format(value) : value}
                                            </TableCell>
                                        );
                                    })}
                                </TableRow>
                            );
                        })}
                    </TableBody>
                </Table>
            </TableContainer>
            <TablePagination
                rowsPerPageOptions={[10, 25, 100]}
                component="div"
                count={rows.length}
                rowsPerPage={rowsPerPage}
                page={page}
                onChangePage={handleChangePage}
                onChangeRowsPerPage={handleChangeRowsPerPage}
                labelRowsPerPage="Записів на сторінці:"
            />
        </Paper>
    );
}