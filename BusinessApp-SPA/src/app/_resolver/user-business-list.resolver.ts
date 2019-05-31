import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Business } from '../_models/Business';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { BusinessService } from '../_services/business.service';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/Auth.service';

@Injectable()
export class UserBusinessListResolver implements Resolve<Business[]> {

    constructor(private businessService: BusinessService, private route: Router,
                private alertify: AlertifyService, private authService: AuthService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Business[]> {
        return this.businessService.getUserBusinesses(this.authService.decodedToken.nameid).pipe(
            catchError(error => {
                this.alertify.error('Problem Retriving data');
                this.route.navigate(['/register']);
                return of(null);
            })
        );
    }
}
