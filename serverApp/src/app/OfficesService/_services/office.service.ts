import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {GetOfficeResponse} from '../_models/responses/GetOfficeResponse'

@Injectable()
export class OfficesService {

    private accessPointUrl: string = 'https://localhost:5000/OfficesApi/Offices';
  
    constructor(private http: HttpClient) {
    }

    getOffices(){
      return this.http.get<GetOfficeResponse[]>(this.accessPointUrl)
    }
  }