import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { PaymentService } from '../_services/payment.service';
import { AuthService } from '../_services/Auth.service';
import { catchError } from 'rxjs/operators';
import { AlertifyService } from '../_services/alertify.service';

@Injectable()
export class PaymentResolver implements Resolve<any> {
    constructor(private route: Router, private paymentService: PaymentService,
                private authService: AuthService, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<any> {
        return this.paymentService.getPaymentParams(this.authService.decodedToken.nameid).pipe(
            catchError(error => {
                this.alertify.error(error);
                this.route.navigate(['']);
                return of(null);
            })
        );
    }
}
