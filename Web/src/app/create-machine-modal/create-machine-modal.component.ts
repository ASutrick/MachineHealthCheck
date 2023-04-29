import { Component } from '@angular/core';
//import { ToastrService } from 'ngx-toastr';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { DataService } from '../services/data-service.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MachineInfo } from '../interfaces/MachineInfo';

@Component({
  selector: 'app-create-machine-modal',
  templateUrl: './create-machine-modal.component.html',
  styleUrls: ['./create-machine-modal.component.css']
})
export class CreateMachineModalComponent {

  newMachineForm = new FormGroup({
    name : new FormControl(),
    machine : new FormControl(),
    key : new FormControl()
  })

  constructor(
    public dataService: DataService,
    private modalService: MdbModalService,
    private formBuilder: FormBuilder,
    //public toastr: ToastrService,
    public modalRef: MdbModalRef<CreateMachineModalComponent>
  ) {}

  ngOnInit(): void {
    
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
      // this.toastr.success("Machine successfully added", "Success", {
      //   timeOut: 5000,
      //   progressBar: true
      // })
    })
  }
}