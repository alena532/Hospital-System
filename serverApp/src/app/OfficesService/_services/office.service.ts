import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Office} from '../_models/Office'

@Injectable()
export class OfficesService {

    private accessPointUrl: string = 'https://localhost:5004/api/Offices';
  
    constructor(private http: HttpClient) {
    }

    getOffices(){
      return this.http.get<Office[]>(this.accessPointUrl)
    }
  }