import { Component, OnInit, ChangeDetectorRef, NgZone, ViewChild, ElementRef } from '@angular/core';
import { DataService } from '../services/data-service.service';
import { MachineInfo } from '../interfaces/MachineInfo';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { MachineInfoModalComponent } from '../machine-info-modal/machine-info-modal.component';
import { CreateMachineModalComponent } from '../create-machine-modal/create-machine-modal.component';
import {MatTableDataSource} from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})

export class HomePageComponent implements OnInit {
  @ViewChild('filter', {static: false}) filter: ElementRef | undefined;
  public selectedMachines: MachineInfo[] = [];
  public selectedMachine: MachineInfo | undefined;
  public toggleIcon = false;
  public displayedColumns = ["ClientName", "MachineName", "Key", "Last Check-In", "CheckServer", "Delete"];
  public allMachineData: any;
  public machineDataSource = new MatTableDataSource;
  public modalRef: MdbModalRef<MachineInfoModalComponent> | null = null;
  public createModalRef: MdbModalRef<CreateMachineModalComponent> | null = null;
  
  constructor (
    public dataService: DataService,
    private modalService: MdbModalService,
    public changeDetectorRefs: ChangeDetectorRef,
    public toastr: ToastrService
  ) { }
  
  ngOnInit(): void {
    this.getAllMachines();
  }

  toggleDarkTheme(): void {
    document.body.classList.toggle("dark-theme");
 }

 getAllMachines(): void {
  this.dataService.getAllMachines().subscribe((res) => {
    this.selectedMachines = res;
    this.allMachineData = res;
    this.machineDataSource = this.allMachineData;
    this.changeDetectorRefs.markForCheck();
    console.log(this.machineDataSource);
  });
 }

 deleteSelectedMachine(key: string, machine: string): void {
  this.dataService.deleteMachine(key).subscribe((res) => {
    console.log("The machine with key: " + key + " has been deleted.");
    this.getAllMachines();
    this.toastr.success(machine + " has been successfully deleted.", "Machine Deleted:", {
      timeOut: 3000,
      progressBar: true
    })
  })
 }

 openCheckServerModal(data: any) {
  this.modalRef = this.modalService.open(MachineInfoModalComponent, {
    data: {name: data.name, machine: data.machine, lastChecked: data.lastChecked, key: data.key}
  });
  this.modalRef.onClose.subscribe(() => {
    this.getAllMachines();
  });
    console.log(data.name);
    console.log(data.lastChecked);
    console.log(this.selectedMachine);
  }

  openCreateMachineModal () {
    this.modalRef = this.modalService.open(CreateMachineModalComponent);
    this.modalRef.onClose.subscribe((newMachine: any) => {
      console.log(newMachine);
      this.getAllMachines();
      this.toastr.success(newMachine.Machine + " has been successfully created.", "Machine Created:", {
        timeOut: 3000,
        progressBar: true
      })
    });
  }
}