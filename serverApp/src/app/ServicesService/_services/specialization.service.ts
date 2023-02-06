import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { GetSpecializationResponse } from '../_models/responses/GetSpecializationResponse';


@Injectable()
export class SpecializationService {

    private accessPointUrl: string = 'https://localhost:5000/ServicesApi/Specializations';
  
    constructor(private http: HttpClient) {
    }

    getSpecializations(){
      return this.http.get<GetSpecializationResponse[]>(this.accessPointUrl)
    }
  }