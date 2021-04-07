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
import { Button } from 'reactstrap';

const checkVisiting = 
        <div>
            <Button variant="success">Success</Button>{' '}
            <Button variant="warning">Warning</Button>
        </ div>

const columns = [
    { id: 'id', label: 'Номер', minWidth: 15 },
    { id: 'name', label: 'ПІБ', minWidth: 240 },
    { id: 'email', label: 'Пошта', minWidth: 190 },
    { id: 'group', label: 'Група', minWidth: 70 },
];

function createData(id, name, email, group) {
    return { id, name, email, group };
}

const numberOfGroup = "301";
const rows = [
    createData(1, 'Мельничук Станіслав Валерійович', 'melnychuk.stanislav@chnu.edu.ua', numberOfGroup),
    createData(2, 'Орелецький Валентин Віталійович', 'oreletskyy.valentyn@chnu.edu.ua', numberOfGroup),
    createData(3, 'Продан Анатолій Сергійович', 'prodan.anatoliy@chnu.edu.ua', numberOfGroup),
    createData(4, 'Роєк Анастасія Іванівна', 'roiek.anastasiia@chnu.edu.ua', numberOfGroup),
    createData(5, 'Стрільчук Вадим Анатолійович', 'strilchuk.vadym@chnu.edu.ua', numberOfGroup),
    createData(6, 'Романовський Михайло Олександрович', 'romanovskyy.mykhaylo@chnu.edu.ua', numberOfGroup),
    createData(7, 'Щур Олександр Іванович', 'shchur.oleksandr@chnu.edu.ua', numberOfGroup),
    createData(8, 'Чебан Владислав Валентинович', 'cheban.vladyslav.v@chnu.edu.ua', numberOfGroup),
    createData(9, 'Тихович Михайло Дмитрович', 'tykhovych.mykhailo@chnu.edu.ua', numberOfGroup),
    createData(10, 'Бурак Денис Павлович', 'burak.denys@chnu.edu.ua', numberOfGroup),
    createData(11, 'Бурденюк Ігор Олександрович', 'burdenyuk.ihor@chnu.edu.ua', numberOfGroup),
    createData(12, 'Гаврилюк Микола Павлович', 'havrylyuk.mykola@chnu.edu.ua', numberOfGroup),
    createData(13, 'Головач Дмітрій Миколайович', 'holovach.dmitriy@chnu.edu.ua', numberOfGroup),
    createData(14, 'Дем\'ян Анастасія Юріївна', 'demian.anastasiia@chnu.edu.ua', numberOfGroup),
    createData(15, 'Думітрюк Юрій Юрійович', 'dumitryuk.yuriy@chnu.edu.ua', numberOfGroup),
    createData(16, 'Жук Василь Васильович', 'zhuk.vasyl@chnu.edu.ua', numberOfGroup),
    createData(17, 'Мадей Андрій Олександрович', 'madey.andriy@chnu.edu.ua', numberOfGroup),
    createData(18, 'Мар\'янчук Олександра Олександрівна', 'marianchuk.oleksandra@chnu.edu.ua', numberOfGroup),
];

const useStyles = makeStyles({
    root: {
        width: '100%',
    },
    container: {
        maxHeight: 590,
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