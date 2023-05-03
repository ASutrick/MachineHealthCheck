import { ChangeDetectorRef, Component, Input } from '@angular/core';
import { MdbModalRef } from 'mdb-angular-ui-kit/modal';
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
  public isLoading: boolean = false;
  public startCheck: boolean = false;
  public recentMachine!: HealthCheckInfo;
  public recentMachineData: any = [];
  public selectedMachines: MachineInfo[] = [];
  
  constructor(
    public dataService: DataService,
    public changeDetectorRefs: ChangeDetectorRef,
    public modalRef: MdbModalRef<MachineInfoModalComponent>
  ) {}

  ngOnInit(): void {
    if(this.lastChecked != null) {
      this.getRecentMachine(this.key);
    }
  }
   
  formatBytes(bytes: any) {
    var marker = 1000; // Change to 1000 if required
    var decimal = 0; // Change as required
    var kiloBytes = marker; // One Kilobyte is 1024 bytes
    var megaBytes = marker * marker; // One MB is 1024 KB
    var gigaBytes = marker * marker * marker; // One GB is 1024 MB
    var teraBytes = marker * marker * marker * marker; // One TB is 1024 GB
    // return bytes if less than a KB
    if(bytes < kiloBytes) return bytes + " Bytes";
    // return KB if less than a MB
    else if(bytes < megaBytes) return(bytes / kiloBytes).toFixed(decimal) + " GB";
    // return MB if less than a GB
    else if(bytes < gigaBytes) return(bytes / megaBytes).toFixed(decimal) + " MB";
    // return GB if less than a TB
    else return(bytes / gigaBytes).toFixed(decimal) + " GB";
  }

  getRecentMachine(key: string): void {
    this.dataService.getMostRecentMachine(key).subscribe((res) => {
      this.recentMachineData = res;
      this.recentMachine = res;
    })
  }

  getAllMachines(): void {
    this.dataService.getAllMachines().subscribe((res) => {
      this.selectedMachines = res;
      this.changeDetectorRefs.markForCheck();
    });
  }

  startMachineHealthCheck(key: string): void {
    this.isLoading = true;
    this.startCheck = true;
    this.dataService.waitWorkQueue(key).subscribe((res) => {
      this.selectedKey = res;
      this.getRecentMachine(key);
      this.isLoading = false;
      this.startCheck = false;
    });
  }

  closeModal(): void {
    if (this.modalRef.onClose) {
      this.dataService.stopWorkQueue().subscribe((res) => {
        console.log(res);
      });
    }
    this.modalRef.close();
  }
}