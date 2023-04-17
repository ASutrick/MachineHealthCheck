import { Component, OnInit } from '@angular/core';
import { DataService } from '../services/data-service.service';
import { MachineInfo } from '../interfaces/MachineInfo';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { MachineInfoModalComponent } from '../machine-info-modal/machine-info-modal.component';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})

export class HomePageComponent implements OnInit {
  public selectedMachines: MachineInfo[] = [];
  public selectedMachine: MachineInfo | undefined;
  public toggleIcon = false;
  public displayedColumns = ["ClientName", "MachineName", "Key", "Last Check-In", "CheckServer", "Delete"];
  public allMachineData: any = [];
  public modalRef: MdbModalRef<MachineInfoModalComponent> | null = null;
  public name = "";
  
  constructor (
    public dataService: DataService,
    private modalService: MdbModalService
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

 openModal(data: any) {
  this.modalRef = this.modalService.open(MachineInfoModalComponent, {
    data: {name: data.name, machine: data.machine, lastChecked: data.lastChecked, key: data.key}
  });
  console.log(data.name);
  console.log(this.selectedMachine);
}

}
