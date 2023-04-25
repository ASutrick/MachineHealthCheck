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
          console.log(res);
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

  public startWorkQueue(key: string): Observable<any> {
    const params = new HttpParams()
    .set ('key', key);

    return this.http.post(`${this.urlString}WorkQueue/Create?key=`, {'params':params})
      .pipe(
        map((res:any) => {
          console.log(res);
          return res;
      })
    )
  }
}
