import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Office} from '../_models/Office'
@Injectable()
export class PatientProfilesService {

  private accessPointUrl: string = 'https://localhost:7281/api/Profiles/CreatePatientProfile';

  constructor(private http: HttpClient) {
  }

  createProfile(data:any) {
    console.log(data)
    return this.http.post<any>(this.accessPointUrl, data).pipe(
    )
  }

  getOffices(){
    return this.http.get<Office[]>('https://localhost:5004/api/Offices')
  }
}