import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Business } from '../_models/Business';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BusinessService {

  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getBusinesses(page?, itemsPerPage?, userParams?): Observable<PaginatedResult<Business[]>> {
    const paginatedResult = new PaginatedResult<Business[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    if (userParams != null) {
      params = params.append('searchQuery', userParams.searchQuery);
      params = params.append('userId', userParams.userId);
    }

    return this.http.get<Business[]>(this.baseUrl + 'businesses', { observe: 'response', params})
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }

          return paginatedResult;
        })
      );
  }

  getUserBusinesses(userId: number): Observable<Business[]> {
    return this.http.get<Business[]>(this.baseUrl + 'users/' + userId + '/businesses');
  }

  getUserBusiness(userId: number, id: number): Observable<Business> {
    return this.http.get<Business>(this.baseUrl + 'users/' + userId + '/businesses/' + id);
  }

  getBusiness(id: number): Observable<Business> {
    return this.http.get<Business>(this.baseUrl + 'businesses/' + id);
  }

  createBusiness(business: Business) {
    return this.http.post<Business>(this.baseUrl + 'businesses', business);
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
