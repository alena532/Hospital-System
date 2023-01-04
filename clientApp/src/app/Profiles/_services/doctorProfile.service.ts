import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Doctor} from '../_models/Doctor';
import { HttpParams } from '@angular/common/http';
import {map, Observable, of, throwError} from "rxjs";
import { Pagination } from '../_models/Pagination';
@Injectable()
export class DoctorProfilesService {

  private accessPointUrl: string = 'https://localhost:7097/api/DoctorProfiles';

  constructor(private http: HttpClient) {
  }

  getAll(pageNumber:number,pageSize:number) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("pageNumber",pageNumber);
    queryParams = queryParams.append("pageSize",pageSize);
    

    return this.http.get<any>(this.accessPointUrl,{params:queryParams})

  }
}