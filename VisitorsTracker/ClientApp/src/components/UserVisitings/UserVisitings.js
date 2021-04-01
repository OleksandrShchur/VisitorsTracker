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
        year: 1993, tvNews: 69, church: 79, military: 72,
    }, {
        year: 1995, tvNews: 53, church: 92, military: 33,
    }, {
        year: 1997, tvNews: 64, church: 95, military: 30,
    }, {
        year: 1999, tvNews: 73, church: 92, military: 34,
    }, {
        year: 2001, tvNews: 85, church: 98, military: 52,
    }, {
        year: 2003, tvNews: 56, church: 97, military: 48,
    }, {
        year: 2006, tvNews: 92, church: 58, military: 49,
    }, {
        year: 2008, tvNews: 71, church: 86, military: 65,
    }, {
        year: 2010, tvNews: 60, church: 75, military: 44,
    }, {
        year: 2012, tvNews: 71, church: 65, military: 23,
    }, {
        year: 2014, tvNews: 80, church: 75, military: 89,
    }, {
        year: 2016, tvNews: 88, church: 60, military: 81,
    }, {
        year: 2018, tvNews: 80, church: 50, military: 83,
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
                        valueField="tvNews"
                        argumentField="year"
                        seriesComponent={Line}
                    />
                    <LineSeries
                        name="Лабораторні"
                        valueField="church"
                        argumentField="year"
                        seriesComponent={Line}
                    />
                    <LineSeries
                        name="Семінари"
                        valueField="military"
                        argumentField="year"
                        seriesComponent={Line}
                    />

                    <Legend position="bottom" rootComponent={Root} itemComponent={Item} labelComponent={Label} />
                    <Title
                        text="Energy Consumption in 2004\n(Millions of Tons, Oil Equivalent)"
                        textComponent={Text}
                    />
                    <Animation />
                </Chart>
            </Paper>
        );
    }
}

export default withStyles(demoStyles, { name: 'Demo' })(Demo);
