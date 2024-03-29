import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class AccountsService {

  private accessPointUrl: string = 'https://localhost:7097/api/Accounts';

  constructor(private http: HttpClient) {
  }

  getByUserId(userId:string) {
    return this.http.get<any>(this.accessPointUrl+ `GetByUserId/${userId}`);
  }

  checkPatientAccountBeforeCreation(id:string) {
    return this.http.get<any>(this.accessPointUrl+`/CheckPatientAccountBeforeProfileCreation/${id}`);
  }
}
