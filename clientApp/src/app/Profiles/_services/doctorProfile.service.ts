import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpParams } from '@angular/common/http';
@Injectable()
export class DoctorProfilesService {

  private accessPointUrl: string = 'https://localhost:7097/api/DoctorProfiles';

  constructor(private http: HttpClient) {
  }

  getAll(pageNumber:number,pageSize:number,firstName:string ,lastName:any,office:number) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("pageNumber",pageNumber);
    queryParams = queryParams.append("pageSize",pageSize);
    queryParams = queryParams.append("firstName",firstName);
    if(lastName != null)
      queryParams = queryParams.append("lastName",lastName);
    queryParams = queryParams.append("office",office);

    return this.http.get<any>(this.accessPointUrl,{params:queryParams})

  }
}
