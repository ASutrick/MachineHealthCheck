import { Component, Inject, Input } from '@angular/core';
import { MdbModalRef } from 'mdb-angular-ui-kit/modal';
import { MachineInfo } from '../interfaces/MachineInfo';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import { HealthCheckInfo } from '../interfaces/HealthCheckInfo';
import { DataService } from '../services/data-service.service';

@Component({
  selector: 'app-machine-info-modal',
  templateUrl: './machine-info-modal.component.html',
  styleUrls: ['./machine-info-modal.component.css']
})
export class MachineInfoModalComponent {
  @Input() data: string = "";
  public name: string = "";
  public machine: string = "";
  public lastChecked: string = "";
  public key: string = "";
  public selectedKey: string = "";
  public recentMachine!: HealthCheckInfo;
  public recentMachineData: any = [];
  
  constructor(
    public dataService: DataService,
    public modalRef: MdbModalRef<MachineInfoModalComponent>
  ) {}

  ngOnInit(): void {
    console.log(this.name + ",\n" + this.machine + ",\n" + this.lastChecked + ",\n" + this.key);
    this.getRecentMachine(this.key);
  }

  getRecentMachine(key: string): void {
    this.dataService.getMostRecentMachine(key).subscribe((res) => {
      this.recentMachineData = res;
      this.recentMachine = res;
      console.log(this.recentMachineData);
    })
  }

  startMachineHealthCheck(): void {
    this.dataService.startWorkQueue(this.key).subscribe((res) => {
      this.selectedKey = res;
      console.log(this.selectedKey);
    })
  }
}