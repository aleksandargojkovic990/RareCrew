import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { EmployeeTableComponent } from './components/employee-table/employee-table.component';
import { EmployeeChartComponent } from './components/employee-chart/employee-chart.component';

@NgModule({
  declarations: [
    AppComponent,
    EmployeeTableComponent,
    EmployeeChartComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
