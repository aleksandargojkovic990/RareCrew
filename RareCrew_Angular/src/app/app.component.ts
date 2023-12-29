import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'RareCrew_Angular';
  private employees: any = []
  public sortedEmployees: any = [];
  private tableInitialized = false;

  constructor(private http: HttpClient) {
  } 

  ngOnInit(): void {
    this.getEmployees();
    this.tableInitialized = false;
  }

  getEmployees() {
    const url ='https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ=='
    this.http.get(url).subscribe((res) => { 
      this.employees = res;
      
      if(!this.tableInitialized)
      {
        this.sortEmployees();
        this.tableInitialized = true;
      }
    })
  }

  sortEmployees() {
    this.getEmployees();
    let totalTimeWorked: number = 0;
    this.sortedEmployees = [...this.employees].map((employee: any) => {
      const startTime = new Date(employee.StarTimeUtc);
      const endTime = new Date(employee.EndTimeUtc);
      
      employee.TotalTimeWorked = (endTime.getTime() - startTime.getTime()) / (1000 * 60 * 60);
      totalTimeWorked += employee.TotalTimeWorked;

      return employee;
    });
  
    let ttw = 0;
    this.sortedEmployees = this.sortedEmployees
      .reduce((acc: any[], employee: any) => {
        const existingEmployee = acc.find(e => e.EmployeeName === employee.EmployeeName);
        
        if (existingEmployee) {
          existingEmployee.TotalTimeWorked += employee.TotalTimeWorked || 0;
        } else {
          acc.push({ ...employee });
        }

        return acc;
      }, [])
      .map((employee: any) => {
        ttw = employee.TotalTimeWorked || 0;
        
        return {
          ...employee,
          TotalTimeWorkedPercentage: (ttw / totalTimeWorked) * 100
        };
      })
      .sort((a: any, b: any) => (b.TotalTimeWorked || 0) - (a.TotalTimeWorked || 0));
  }
}
