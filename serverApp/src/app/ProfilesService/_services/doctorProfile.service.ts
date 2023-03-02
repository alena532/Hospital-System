import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpParams } from '@angular/common/http';
import { GetDoctorProfilesResponse } from '../_models/responses/GetDoctorProfilesResponse';
import { EditDoctorProfileAndPhoto } from '../_models/requests/EditDoctorProfileAndPhoto';
import { catchError } from 'rxjs';
@Injectable()
export class DoctorProfilesService {

  private accessPointUrl: string = 'https://localhost:5000/ProfilesApi/DoctorProfiles';

  constructor(private http: HttpClient) {
  }

  getAll(pageNumber:number,pageSize:number,firstName:string | null,lastName:string | null,officeId:string | null) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("pageNumber",pageNumber);
    queryParams = queryParams.append("pageSize",pageSize);
    if(firstName != null)
      queryParams = queryParams.append("firstName",firstName);
    if(lastName != null)
      queryParams = queryParams.append("lastName",lastName);
    if(officeId != null)
      queryParams = queryParams.append("officeId",officeId);

    return this.http.get<any>(this.accessPointUrl,{params:queryParams})
  }

  getById(id:string){
    return this.http.get<GetDoctorProfilesResponse>(this.accessPointUrl+`/${id}`);
  }

  update(editForm:FormData){
    return this.http.put("https://localhost:5000/Orchestrator/DoctorProfiles",editForm)
    .pipe()
  }

  create(createForm:FormData){
    return this.http.post("https://localhost:5000/Orchestrator/DoctorProfiles",createForm);
  }
}
