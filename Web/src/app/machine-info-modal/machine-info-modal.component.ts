import { ChangeDetectorRef, Component, Inject, Input } from '@angular/core';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { HealthCheckInfo } from '../interfaces/HealthCheckInfo';
import { DataService } from '../services/data-service.service';
import { MachineInfo } from '../interfaces/MachineInfo';

const units = ['bytes', 'KiB', 'MiB', 'GiB', 'TiB', 'PiB', 'EiB', 'ZiB', 'YiB'];

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
    public changeDetectorRefs: ChangeDetectorRef,
    public modalRef: MdbModalRef<MachineInfoModalComponent>
  ) {}

  ngOnInit(): void {
    console.log(this.name + ",\n" + this.machine + ",\n" + this.lastChecked + ",\n" + this.key);
    this.getRecentMachine(this.key);
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
    else if(bytes < megaBytes) return(bytes / kiloBytes).toFixed(decimal) + " KB";
    // return MB if less than a GB
    else if(bytes < gigaBytes) return(bytes / megaBytes).toFixed(decimal) + " MB";
    // return GB if less than a TB
    else return(bytes / gigaBytes).toFixed(decimal) + " GB";
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