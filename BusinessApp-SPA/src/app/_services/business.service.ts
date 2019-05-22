import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Business } from '../_models/Business';

@Injectable({
  providedIn: 'root'
})
export class BusinessService {

  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getBusinesses(): Observable<Business[]> {
    return this.http.get<Business[]>(this.baseUrl + 'businesses');
  }

  getBusiness(id: number): Observable<Business> {
    return this.http.get<Business>(this.baseUrl + 'businesses/' + id);
  }

}
