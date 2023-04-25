import { ChangeDetectorRef, Component, Inject, Input } from '@angular/core';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { HealthCheckInfo } from '../interfaces/HealthCheckInfo';
import { DataService } from '../services/data-service.service';
import { MachineInfo } from '../interfaces/MachineInfo';

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
  public selectedMachines: MachineInfo[] = [];
  
  constructor(
    public dataService: DataService,
    private modalService: MdbModalService,
    public changeDetectorRefs: ChangeDetectorRef,
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

  getAllMachines(): void {
    this.dataService.getAllMachines().subscribe((res) => {
      this.selectedMachines = res;
      this.changeDetectorRefs.markForCheck();
      console.log(this.selectedMachines);
    });
   }

  startMachineHealthCheck(key: string): void {
    this.dataService.startWorkQueue(key).subscribe((res) => {
      this.selectedKey = res;
      console.log(this.selectedKey);
    })
  }

  closeModal(): void {
    this.modalRef.close();
  }
}