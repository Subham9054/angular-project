import { Component } from '@angular/core';
import * as Highcharts from 'highcharts';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {


  // High chart location
  Highcharts: typeof Highcharts = Highcharts;
  chartOptions: Highcharts.Options = {
    chart: {
      type: 'column',
    },

    title: {
      text: '',
    },
    xAxis: {
      categories: ['ARARIA', 'ARWAL', 'AURANGABAD', 'BANKA', 'BEGUSARAI', 'BHAGALPUR',
        'BUXAR', 'DARBHANGA', 'GAYA', 'GOPALGANJ', 'JAMUI', 'JEHANABAD',
        'KAIMUR', 'KATIHAR', 'KHAGARIA', 'KISHANGANJ', 'LAKHISARAI', 'MADHEPURA',
        'MADHUBA', 'MUNGER', 'MUZAFFARPUR', 'NALANDA', 'NAWADA', 'PASHCHIM CHAMPARAN',
        'PATNA', 'PURBI CHAMPARAN', 'PURNIA', 'ROHTAS', 'SAHARSA', 'SAMASTIPUR',
        'SARAN', 'SHEIKHPURA', 'SHEOHAR', 'SITAMARHI', 'SIWAN', 'SUPAUL',
        'VAISHALI', 'Grand Total'
      ],
    },
    yAxis: {
      min: 0,
      title: {
        text: 'Number of Complaints',
      },
    },
    plotOptions: {
      column: {
        grouping: true,
        shadow: false,
      },
    },
    series: [
      {
        type: 'column',
        name: 'Received',
        data: [5, 3, 4, 5, 7, 6, 5, 3, 4, 5, 7, 6, 5, 3, 4, 5, 7, 6, 5, 3, 4, 5, 7, 6, 5, 3, 4, 5, 7, 6, 5, 3, 4, 5, 7, 6, 8, 9],
        color: '#7cb5ec',
      },
      {
        type: 'column',
        name: 'Pending',
        data: [2, 4, 6, 3, 4, 7, 2, 4, 6, 3, 4, 7, 2, 4, 6, 3, 4, 7, 2, 4, 6, 3, 4, 7, 2, 4, 6, 3, 4, 7, 2, 4, 6, 3, 4, 7, 8, 9],
        color: '#434348',
      },
      {
        type: 'column',
        name: 'Resolved',
        data: [3, 5, 7, 4, 5, 4, 3, 5, 7, 4, 5, 4, 3, 5, 7, 4, 5, 4, 3, 5, 7, 4, 5, 4, 3, 5, 7, 4, 5, 4, 3, 5, 7, 4, 5, 4, 9, 10],
        color: '#90ed7d',
      },
    ],
    credits: {
      enabled: false,
    },
  };
  // High chart Category
  Highcharts1: typeof Highcharts = Highcharts;
  container: Highcharts.Options = {
    credits: {
      enabled: false,
    },
    xAxis: {
      categories: [
        'Handpump Tubewell Related',
        'Piped Water Supply Related',
        'Water Quality Related',
        'Miscellaneous',
        'Grand Total',
      ],
    },
    yAxis: {
      min: 0,
      title: {
        text: 'Number of Complaints',
      },
    },
    title: {
      text: '',
    },
    series: [{
      name: 'Recieved',
      type: 'line',
      color: '#7cb5ec',
      data: [5, 8, 12, 3, 28], 
      marker: {
        enabled: true,
        radius: 6,
      },
    }, {
      name: 'Pending',
      type: 'line',
      color: '#434348',
      data: [15, 20, 30, 5, 70], 
      marker: {
        enabled: true,
        radius: 6,
      },
    }, {
      name: 'Resolved',
      type: 'line',
      color: '#90ed7d',
      data: [10, 12, 15, 2, 40], 
      marker: {
        enabled: true,
        radius: 6,
      },
    }],

  }
};
