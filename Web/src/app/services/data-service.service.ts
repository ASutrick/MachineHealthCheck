import { Injectable } from '@angular/core';
import { MachineInfo } from '../interfaces/MachineInfo';
import { environment } from '../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { HealthCheckInfo } from '../interfaces/HealthCheckInfo';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private urlString: string = environment.portalApiUrl;

  constructor(
    public http: HttpClient
  ) { }

  public getAllMachines(): Observable<MachineInfo[]> {
    return this.http.get(`${this.urlString}MachineInfo/ListMachineInfos`)
      .pipe(
        map((res:any) => {
          return res;
      })
    )
  }

  public getAllHealthChecks(key: string): Observable<HealthCheckInfo> {
    return this.http.get(`${this.urlString}HealthCheck/List?key=` + key)
      .pipe(
        map((res:any) => {
          return res;
      })
    )
  }

  public getMostRecentMachine(key: string): Observable<HealthCheckInfo> {
    const params = new HttpParams()
    .set ('key', key);
    
    return this.http.get(`${this.urlString}HealthCheck/MostRecent`, {params: params})
      .pipe(
        map((res:any) => {
          return res;
      })
    )
  }

  public startWorkQueue(key: string) {
    return this.http.post(`${this.urlString}WorkQueue/Create?key=` + key, null)
      .pipe(
        map((res:any) => {
          return res;
      })
    )
  }

  public waitWorkQueue(key: string) {
    return this.http.post(`${this.urlString}WorkQueue/WaitCreate?key=` + key, null)
      .pipe(
        map((res:any) => {
          return res;
      })
    )
  } 

  public createNewMachine(model: MachineInfo): Observable<MachineInfo> {
    return this.http.post<any>(`${this.urlString}MachineInfo/Create`, model)
      .pipe(
        map((res:any) => {
          return res;
      })
    )
  }

  public deleteMachine (key: string) {
    return this.http.delete(`${this.urlString}MachineInfo/Delete?key=` + key)
      .pipe(
        map((res:any) => {
          return res;
      })
    )
  }

  public stopWorkQueue() {
    return this.http.delete(`${this.urlString}WorkQueue/Delete`)
      .pipe(
        map((res:any) => {
          return res;
      })
    )
  }
}
