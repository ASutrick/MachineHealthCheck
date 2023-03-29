import { Component, OnInit } from '@angular/core';
import {MatTableDataSource, MatTableModule} from '@angular/material/table'
import { MatIcon } from '@angular/material/icon';
import { DataService } from '../services/data-service.service';
import { MachineInfo } from '../interfaces/MachineInfo';

const MACHINE_DATA: MachineInfo[] = [
  {machineId: 'FABC1DE0-55D1-4BE7-AF2C-34C069861FE9', clientName: 'client', machineName: 'Jessika', dateChecked: '2023-03-12 17:56:35.6019816'}
];

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})

export class HomePageComponent implements OnInit {

  public machines: MachineInfo[] = []
  public displayedColumns = ['clientName', 'machineName', 'dateChecked', 'checkServer', 'delete'];
  public dataSource = new MatTableDataSource(MACHINE_DATA);
  
  constructor (
    public dataService: DataService
  ) { }
  
  ngOnInit(): void {
    //this.getMachineData();
    console.log(this.getMachineData());
  }

  public toggleIcon = false;

  //Toggle dark and light mode
  toggleDarkTheme(): void {
    console.log("You clicked me!");
    document.body.classList.toggle("dark-theme");
 }

 getMachineData(): void {
  this.dataService.getMachines().subscribe((res) => {
    res = this.machines;
  })
 }

}
