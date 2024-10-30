
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'; 
import { Truck } from '../types/truck'


@Injectable({
    providedIn: 'root'
  })
 

export class TruckService{      

    truck :any;

    constructor(protected http: HttpClient) {
        this.httpOptions.headers.append('Access-Control-Allow-Origin', '*');
        this.httpOptions.headers.append('Content-Type', 'multipart/form-data');
        this.httpOptions.headers.append('Accept', 'application/json');      
        this.truck = new Truck(0, "", "", new Date(), "", "", ""); 

      }
    

    private apiUrl = 'https://localhost:7099/api/Trucks';
    protected httpOptions = { headers : new HttpHeaders() };

    getAll = ():Observable<Array<any>> => {
        return this.http.get<Array<any>>(this.apiUrl);           
    }

    getTruck = (id:any):Observable<any> => {
        return this.http.get<any>(this.apiUrl+"/"+id, this.httpOptions);           
    }

    postTruck = (truck:any):Observable<any> => {
        return this.http.post<any>(this.apiUrl, truck, this.httpOptions);
    }

    deleteTruck = (id:any):Observable<any> => {
        return this.http.delete<any>(this.apiUrl+"/"+id, this.httpOptions);
    }

    getTruckModels = ():Observable<any> => {
        return this.http.get<any>(this.apiUrl+"/GetModels", this.httpOptions);           
    }

    getSites = ():Observable<any> => {
        return this.http.get<any>(this.apiUrl+"/GetSites", this.httpOptions);           
    }
}
