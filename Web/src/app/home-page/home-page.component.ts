import { Component, OnInit } from '@angular/core';
import { DataService } from '../services/data-service.service';
import { MachineInfo } from '../interfaces/MachineInfo';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})

export class HomePageComponent implements OnInit {
  public selectedMachines: MachineInfo[] = [];
  public selectedMachine: MachineInfo | undefined;
  public toggleIcon = false;
  public displayedColumns = ["ClientName", "MachineName", "Key", "CheckServer", "Delete"];
  public allMachineData: any = [];
  
  constructor (
    public dataService: DataService
  ) { }
  
  ngOnInit(): void {
    console.log("The current Machine Data: " + this.getAllMachines());
    this.getAllMachines();
  }

  toggleDarkTheme(): void {
    console.log("You clicked me!");
    document.body.classList.toggle("dark-theme");
 }

 getAllMachines(): void {
  this.dataService.getAllMachines().subscribe((res) => {
    this.allMachineData = res;
    this.selectedMachines = res;
    console.log(res);
    console.log(this.allMachineData);
  });
 }

}
