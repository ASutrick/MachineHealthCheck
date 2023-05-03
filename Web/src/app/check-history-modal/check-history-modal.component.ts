import { ChangeDetectorRef, Component, Input } from '@angular/core';
import { DataService } from '../services/data-service.service';
import { MdbModalRef } from 'mdb-angular-ui-kit/modal';
import { HealthCheckInfo } from '../interfaces/HealthCheckInfo';
import { animate, state, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-check-history-modal',
  templateUrl: './check-history-modal.component.html',
  styleUrls: ['./check-history-modal.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0', visibility: 'hidden' })),
      state('expanded', style({ height: '*', visibility: 'visible' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ]
})
export class CheckHistoryModalComponent {
  @Input() data: string = "";
  public name: string = "";
  public machine: string = "";
  public key: string = "";
  public healthCheckInfo: HealthCheckInfo | undefined;
  public healthCheckData: any;
  public displayedColumns = ["Last Check-In", "Details"];

  constructor(
    public dataService: DataService,
    public changeDetectorRefs: ChangeDetectorRef,
    public modalRef: MdbModalRef<CheckHistoryModalComponent>
  ) {}

  ngOnInit(): void {
    console.log(this.getHealthCheckInfo());
  }

  getHealthCheckInfo() {
    this.dataService.getAllHealthChecks(this.key).subscribe((res) => {
      this.healthCheckInfo = res;
      this.healthCheckData = res;
    });
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

  isExpansionDetailRow = (i: number, row: Object) => row.hasOwnProperty('detailRow');
    expandedElement: any;
    test() {
      console.log('test');
  }

  closeModal(): void {
    this.modalRef.close();
  }
}
