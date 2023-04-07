import { Injectable } from '@angular/core';
import { MachineInfo } from '../interfaces/MachineInfo';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';

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
}
