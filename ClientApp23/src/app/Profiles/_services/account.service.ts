import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class AccountsService {

  private accessPointUrl: string = 'https://localhost:7097/api/admin/Companies';

  constructor(private http: HttpClient) {
  }

  getAll() {
    return this.http.get<any>(this.accessPointUrl);
  }
}