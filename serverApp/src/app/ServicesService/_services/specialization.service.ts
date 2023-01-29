import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Specialization } from '../_models/Specialization';


@Injectable()
export class SpecializationService {

    private accessPointUrl: string = 'https://localhost:7276/api/Specializations';
  
    constructor(private http: HttpClient) {
    }

    getSpecializations(){
      return this.http.get<Specialization[]>(this.accessPointUrl)
    }
  }