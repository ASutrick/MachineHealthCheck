import { Component, OnInit, ChangeDetectorRef, NgZone } from '@angular/core';
import { DataService } from '../services/data-service.service';
import { MachineInfo } from '../interfaces/MachineInfo';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { MachineInfoModalComponent } from '../machine-info-modal/machine-info-modal.component';
import { CreateMachineModalComponent } from '../create-machine-modal/create-machine-modal.component';
import { ToastrService } from 'ngx-toastr';

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
  public allMachineData: any[] = [];
  public modalRef: MdbModalRef<MachineInfoModalComponent> | null = null;
  public createModalRef: MdbModalRef<CreateMachineModalComponent> | null = null;
  public name = "";
  
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
    console.log("You clicked me!");
    document.body.classList.toggle("dark-theme");
 }

 getAllMachines(): void {
  this.dataService.getAllMachines().subscribe((res) => {
    this.selectedMachines = res;
    this.changeDetectorRefs.markForCheck();
    console.log(this.selectedMachines);
  });
 }

 deleteSelectedMachine(key: string, machine: string): void {
  this.dataService.deleteMachine(key).subscribe((res) => {
    console.log("The machine with key: " + key + " has been deleted.");
    this.getAllMachines();
    this.toastr.success("Machine " + machine + " has been successfully deleted.", "Machine Deleted:", {
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
      this.toastr.success("Machine " + newMachine.Machine + " has been successfully created.", "Machine Created:", {
        timeOut: 3000,
        progressBar: true
      })
    });
  }
}