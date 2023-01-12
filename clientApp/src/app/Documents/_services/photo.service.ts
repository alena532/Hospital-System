import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable()
export class PhotosService {

  private accessPointUrl: string = 'https://localhost:7034/api/Photos/';

  constructor(private http: HttpClient) {
  }

  getByPatientId(patientId:string){
    return this.http.get<any>(this.accessPointUrl+ `${patientId}`);
  }

  
}