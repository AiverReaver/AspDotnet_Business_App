import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getPaymentParams(userId: number) {
    return this.http.get(this.baseUrl + 'payments/' + userId + '/pay');
  }

}
