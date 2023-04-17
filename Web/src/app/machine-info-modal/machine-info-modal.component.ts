import { Component, Inject, Input } from '@angular/core';
import { MdbModalRef } from 'mdb-angular-ui-kit/modal';
import { MachineInfo } from '../interfaces/MachineInfo';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'app-machine-info-modal',
  templateUrl: './machine-info-modal.component.html',
  styleUrls: ['./machine-info-modal.component.css']
})
export class MachineInfoModalComponent {
  @Input() data: string | undefined;
  public name: string | undefined;
  public machine: string | undefined;
  public lastChecked: string | undefined;
  public key: string | undefined;
  
  constructor(
    public modalRef: MdbModalRef<MachineInfoModalComponent>
  ) {}

  ngOnInit(): void {
    console.log(this.name + ",\n" + this.machine + ",\n" + this.lastChecked + ",\n" + this.key);
  }
}
