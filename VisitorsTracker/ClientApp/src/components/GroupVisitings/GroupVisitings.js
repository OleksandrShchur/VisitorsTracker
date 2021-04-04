import * as React from 'react';
import Paper from '@material-ui/core/Paper';
import {
    Chart,
    ArgumentAxis,
    ValueAxis,
    LineSeries,
    Title,
    Legend,
} from '@devexpress/dx-react-chart-material-ui';
import { withStyles } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';
import { ArgumentScale, Animation } from '@devexpress/dx-react-chart';
import {
    curveCatmullRom,
    line,
} from 'd3-shape';
import { scalePoint } from 'd3-scale';

const data = [
    {
        week: 1, lecture: 69, laboratory: 79, seminar: 72, practice: 60, visiting: 69,
    }, {
        week: 2, lecture: 53, laboratory: 92, seminar: 33, practice: 80, visiting: 53,
    }, {
        week: 3, lecture: 64, laboratory: 100, seminar: 29, practice: 70, visiting: 64,
    }, {
        week: 4, lecture: 73, laboratory: 92, seminar: 34, practice: 90, visiting: 73,
    }, {
        week: 5, lecture: 85, laboratory: 98, seminar: 52, practice: 70, visiting: 85,
    }, {
        week: 6, lecture: 56, laboratory: 97, seminar: 48, practice: 50, visiting: 56,
    }, {
        week: 7, lecture: 92, laboratory: 58, seminar: 49, practice: 60, visiting: 92,
    }, {
        week: 8, lecture: 71, laboratory: 86, seminar: 65, practice: 70, visiting: 71,
    }, {
        week: 9, lecture: 60, laboratory: 75, seminar: 44, practice: 70, visiting: 60,
    }, {
        week: 10, lecture: 71, laboratory: 65, seminar: 23, practice: 60, visiting: 71,
    }, {
        week: 11, lecture: 80, laboratory: 75, seminar: 89, practice: 80, visiting: 80,
    }, {
        week: 12, lecture: 88, laboratory: 60, seminar: 81, practice: 90, visiting: 88,
    }, {
        week: 13, lecture: 80, laboratory: 50, seminar: 83, practice: 50, visiting: 80,
    }, {
        week: 14, lecture: 89, laboratory: 57, seminar: 83, practice: 70, visiting: 89,
    }, {
        week: 15, lecture: 78, laboratory: 50, seminar: 83, practice: 60, visiting: 78,
    },
];

const Line = props => (
    <LineSeries.Path
        {...props}
        path={line()
            .x(({ arg }) => arg)
            .y(({ val }) => val)
            .curve(curveCatmullRom)}
    />
);

const titleStyles = {
    title: {
        textAlign: 'center',
        width: '100%',
        marginBottom: '10px',
    },
};
const Text = withStyles(titleStyles)((props) => {
    const { text, classes } = props;
    const [mainText, subText] = text.split('\\n');
    return (
        <div className={classes.title}>
            <Typography component="h3" variant="h5">
                {mainText}
            </Typography>
            <Typography variant="subtitle1">{subText}</Typography>
        </div>
    );
});

const legendStyles = () => ({
    root: {
        display: 'flex',
        margin: 'auto',
        flexDirection: 'row',
    },
});
const legendLabelStyles = theme => ({
    label: {
        marginBottom: theme.spacing(1),
        whiteSpace: 'nowrap',
    },
});
const legendItemStyles = () => ({
    item: {
        flexDirection: 'column-reverse',
    },
});

const legendRootBase = ({ classes, ...restProps }) => (
    <Legend.Root {...restProps} className={classes.root} />
);
const legendLabelBase = ({ classes, ...restProps }) => (
    <Legend.Label className={classes.label} {...restProps} />
);
const legendItemBase = ({ classes, ...restProps }) => (
    <Legend.Item className={classes.item} {...restProps} />
);
const Root = withStyles(legendStyles, { name: 'LegendRoot' })(legendRootBase);
const Label = withStyles(legendLabelStyles, { name: 'LegendLabel' })(legendLabelBase);
const Item = withStyles(legendItemStyles, { name: 'LegendItem' })(legendItemBase);
const demoStyles = () => ({
    chart: {
        paddingRight: '30px',
    },
});


class Demo extends React.PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            data,
        };
    }

    render() {
        const { data: chartData } = this.state;
        const { classes } = this.props;

        return (
            <Paper>
                <Chart
                    data={chartData}
                    className={classes.chart}
                >
                    <ArgumentScale factory={scalePoint} />
                    <ArgumentAxis />
                    <ValueAxis />

                    <LineSeries
                        name="Лекції"
                        valueField="lecture"
                        argumentField="week"
                        seriesComponent={Line}
                    />
                    <LineSeries
                        name="Лабораторні"
                        valueField="laboratory"
                        argumentField="week"
                        seriesComponent={Line}
                    />
                    <LineSeries
                        name="Семінари"
                        valueField="seminar"
                        argumentField="week"
                        seriesComponent={Line}
                    />
                    <LineSeries
                        name="Практика"
                        valueField="practice"
                        argumentField="week"
                        seriesComponent={Line}
                    />

                    <Legend position="bottom" rootComponent={Root} itemComponent={Item} labelComponent={Label} />
                    <Title
                        text="Графік відвідуваності студентів групи\n"
                        textComponent={Text}
                    />
                    <Animation />
                </Chart>
            </Paper>
        );
    }
}

export default withStyles(demoStyles, { name: 'Demo' })(Demo);
