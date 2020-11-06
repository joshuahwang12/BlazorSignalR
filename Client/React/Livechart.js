import React, { useEffect, useState } from 'react';
import dotnetify from 'dotnetify';
import { LiveChartCss } from './components/css';
import { Line, Bar, Doughnut } from 'react-chartjs-2'; 
import 'chartjs-plugin-streaming';
import { HubConnectionBuilder } from '@microsoft/signalr'

class LiveChart extends React.Component {
  constructor(props) {
    super(props);
    this.state = { Waveform: [], Bar: [], Pie: [] };
    const [ connection, setConnection ] = useState(null);
    // Connect this component to the back-end view model.
    // this.vm = dotnetify.react.connect('LiveChartVM', this);

    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl('https://localhost:5001/hubs/dotnetifyhub')
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);
    }, []);
    connection.on("Recieve Data", function (data) {
      state.Waveform = data.Waveform;
      state.Bar = data.Bar;
      state.Pie = data.Pie;
    })
  }

  componentWillUnmount() {
    this.vm.$destroy();
  }

  render() {
    return (
      <LiveChartCss>
        <div>
          <LineChart data={this.state.Waveform} />
        </div>
        <div>
          <PieChart data={this.state.Pie} />
          <BarChart data={this.state.Bar} />
        </div>
      </LiveChartCss>
    );
  }
}