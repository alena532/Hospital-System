import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpParams } from '@angular/common/http';
import { GetDoctorProfile } from '../_models/responses/GetDoctorProfile';
import { EditDoctorProfileAndPhoto } from '../_models/requests/EditDoctorProfileAndPhoto';
@Injectable()
export class DoctorProfilesService {

  private accessPointUrl: string = 'https://localhost:7097/api/DoctorProfiles';

  constructor(private http: HttpClient) {
  }

  getAll(pageNumber:number,pageSize:number,firstName:string | null,lastName:string | null,office:number) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("pageNumber",pageNumber);
    queryParams = queryParams.append("pageSize",pageSize);
    if(firstName != null)
      queryParams = queryParams.append("firstName",firstName);
    if(lastName != null)
      queryParams = queryParams.append("lastName",lastName);
    queryParams = queryParams.append("office",office);

    return this.http.get<any>(this.accessPointUrl,{params:queryParams})
  }

  getById(id:string){
    return this.http.get<GetDoctorProfile>(this.accessPointUrl+`/${id}`);
  }
  
  update(editForm:FormData){
    return this.http.put('https://localhost:7281/api/DoctorProfiles',editForm);
   //new EditDoctorProfileAndPhoto(editForm.get('id')!,editForm.get('firstName'),editForm.get('lastName'),editForm.get('middleName'), editForm.get('dateOfBirth'),editForm.get('careerStartYear'),
//editForm.get('status'),editForm.get('specialization')?.id,editForm.get('specialization')?.specializationName,editForm.get('photo'),editForm.get('office')?.adress,editForm.get('office').id,editForm.get('photo')
  //  )
  }
}
