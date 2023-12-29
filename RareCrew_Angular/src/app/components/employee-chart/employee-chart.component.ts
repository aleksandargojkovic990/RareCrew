import { Component, Input, OnChanges, SimpleChanges, OnInit } from '@angular/core';
import Chart from 'chart.js/auto';

@Component({
  selector: 'app-employee-chart',
  templateUrl: './employee-chart.component.html',
  styleUrl: './employee-chart.component.css'
})
export class EmployeeChartComponent implements OnChanges, OnInit {
  @Input() employees: any[] = [];
  private chartInitialized = false;

  ngOnInit(): void {
    this.chartInitialized = false;
  }

  ngOnChanges(changes: SimpleChanges): void  {
    if (this.employees.length > 0 && !this.chartInitialized) {
      this.setupChart();
      this.chartInitialized = true;
    }
  }

  private setupChart() {
    const ctx = document.getElementById('pieChart') as HTMLCanvasElement;
    const data = this.employees;

    const chart = new Chart(ctx, {
      type: 'pie',
      data: {
        labels: data.map(item => item.EmployeeName),
        datasets: [{
          data: data.map(item => item.TotalTimeWorkedPercentage),
          backgroundColor: [
            '#D86057',
            '#E6BCAC',
            '#9A4F3C',
            '#3C2D2A',
            '#BC9F4F',
            '#D7CED1',
            '#334075',
            '#246D73'
          ]
        }]
      },
      options: {
        responsive: false
      }
    });
  }
}