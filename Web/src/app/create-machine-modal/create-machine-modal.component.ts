import { Component } from '@angular/core';
import { MdbModalRef } from 'mdb-angular-ui-kit/modal';
import { DataService } from '../services/data-service.service';
import { FormControl, FormGroup } from '@angular/forms';
import { MachineInfo } from '../interfaces/MachineInfo';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-create-machine-modal',
  templateUrl: './create-machine-modal.component.html',
  styleUrls: ['./create-machine-modal.component.css']
})
export class CreateMachineModalComponent {

  public selectedMachines: MachineInfo[] = [];
  public newMachineForm = new FormGroup({
    name : new FormControl(),
    machine : new FormControl(),
    key : new FormControl()
  })

  constructor(
    public dataService: DataService,
    public toastr: ToastrService,
    public modalRef: MdbModalRef<CreateMachineModalComponent>
  ) {}

  ngOnInit(): void {
    this.getAllMachines();
  }

  getAllMachines(): void {
    this.dataService.getAllMachines().subscribe((res) => {
      this.selectedMachines = res;
      console.log(this.selectedMachines);
    });
  }

  submitNewMachine(): void {
    let newMachine: MachineInfo = {
      Id: "",
      Name: this.newMachineForm.controls['name'].value,
      Machine: this.newMachineForm.controls['machine'].value,
      Key: this.newMachineForm.controls['key'].value,
      LastChecked: undefined,
      IsVerified: false,
      HealthChecks: 0
    };
    this.dataService.createNewMachine(newMachine).subscribe((res) => {
      this.modalRef.close(newMachine);
    }, error => {
      this.toastr.error("Cannot create a machine with the same key.", "Error:", {
        timeOut: 3000,
        progressBar: true
      })
    });
  }
}