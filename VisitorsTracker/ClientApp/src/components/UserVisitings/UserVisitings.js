import * as React from 'react';
import Paper from '@material-ui/core/Paper';
import {
    Chart,
    ArgumentAxis,
    ValueAxis,
    AreaSeries,
    Title,
    Legend,
} from '@devexpress/dx-react-chart-material-ui';
import classNames from 'clsx';
import { withStyles } from '@material-ui/core/styles';
import { Stack, Animation } from '@devexpress/dx-react-chart';
import { stackOffsetExpand } from 'd3-shape';

const data = [
    {
        week: 1, attendant: 69, absent: 31,
    }, {
        week: 2, attendant: 60, absent: 40,
    }, {
        week: 3, attendant: 64, absent: 36,
    }, {
        week: 4, attendant: 73, absent: 27,
    }, {
        week: 5, attendant: 80, absent: 20,
    }, {
        week: 6, attendant: 56, absent: 33,
    }, {
        week: 7, attendant: 88, absent: 12,
    }, {
        week: 8, attendant: 71, absent: 29,
    }, {
        week: 9, attendant: 60, absent: 40,
    }, {
        week: 10, attendant: 71, absent: 29,
    }, {
        week: 11, attendant: 80, absent: 20,
    }, {
        week: 12, attendant: 88, absent: 12,
    }, {
        week: 13, attendant: 80, absent: 20,
    }, {
        week: 14, attendant: 89, absent: 11,
    }, {
        week: 15, attendant: 78, absent: 22,
    },
];

const setStyle = (style) => {
    const wrap = withStyles({ root: style });
    return Target => wrap(({ classes, className, ...restProps }) => (
        <Target className={classNames(classes.root, className)} {...restProps} />
    ));
};

const LegendRoot = setStyle({
    display: 'flex',
    margin: 'auto',
    flexDirection: 'row',
})(Legend.Root);

const LegendLabel = setStyle({
    whiteSpace: 'nowrap',
})(Legend.Label);

const ChartRoot = setStyle({
    paddingRight: '20px',
})(Chart.Root);

const format = () => tick => tick;
const formatForFullstack = scale => scale.tickFormat(null, '%');
const stacks = [{
    series: ['Присутність', 'Відсутність'],
}];

export default class Demo extends React.PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            data,
        };
    }

    render() {
        const { data: chartData } = this.state;
        return (
            <Paper>
                <Chart
                    data={chartData}
                    rootComponent={ChartRoot}
                >
                    <ArgumentAxis tickFormat={format} />
                    <ValueAxis tickFormat={formatForFullstack} />
                    <AreaSeries
                        name="Присутність"
                        valueField="attendant"
                        argumentField="week"
                    />
                    <AreaSeries
                        name="Відсутність"
                        valueField="absent"
                        argumentField="week"
                    />

                    <Animation />
                    <Legend position="bottom" rootComponent={LegendRoot} labelComponent={LegendLabel} />
                    <Title text="Відвідуваність студента" />
                    <Stack stacks={stacks} offset={stackOffsetExpand} />
                </Chart>
            </Paper>
        );
    }
}
