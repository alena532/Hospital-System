import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class PatientProfilesService {

  private accessPointUrl: string = 'https://localhost:7097/api/PatientProfiles';

  constructor(private http: HttpClient) {
  }

  createProfile(data:any) {
    return this.http.post<any>(this.accessPointUrl, data).pipe(
    )
  }
}