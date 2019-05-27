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

  updateBusiness(id: number, business: Business) {
    return this.http.put(this.baseUrl + 'businesses/' + id, business);
  }

  setMainPhoto(businessId: number, id: number) {
    return this.http.post(
      this.baseUrl + 'businesses/' + businessId + '/photos/' + id + '/setMain',
      {}
    );
  }

  deletePhoto(businessId: number, id: number) {
    return this.http.delete(this.baseUrl + 'businesses/' + businessId + '/photos/' + id);
  }

}
