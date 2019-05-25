import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Business } from '../_models/Business';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/Auth.service';
import { UserService } from '../_services/user.service';

@Injectable()
export class UserEditResolver implements Resolve<Business> {
    constructor(private userService: UserService, private route: Router,
                private alertify: AlertifyService, private authService: AuthService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Business> {
        return this.userService.getUser(this.authService.decodedToken.nameid).pipe(
            catchError(error => {
                this.alertify.error('Problem Retriving your data');
                this.route.navigate(['']);
                return of(null);
            })
        );
    }
}
