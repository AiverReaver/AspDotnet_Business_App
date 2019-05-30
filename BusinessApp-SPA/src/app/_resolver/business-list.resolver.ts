import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Business } from '../_models/Business';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { BusinessService } from '../_services/business.service';
import { catchError } from 'rxjs/operators';

@Injectable()
export class BusinessListResolver implements Resolve<Business[]> {
    pageNumber = 1;
    pageSize = 5;

    constructor(private businessService: BusinessService, private route: Router,
                private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Business[]> {
        return this.businessService.getBusinesses(this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                this.alertify.error('Problem Retriving data');
                this.route.navigate(['/register']);
                return of(null);
            })
        );
    }
}
