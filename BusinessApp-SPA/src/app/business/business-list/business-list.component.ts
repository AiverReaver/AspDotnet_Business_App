import { Component, OnInit } from '@angular/core';
import { Business } from '../../_models/Business';
import { BusinessService } from '../../_services/business.service';
import { AlertifyService } from '../../_services/alertify.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { AuthService } from 'src/app/_services/Auth.service';

@Component({
  selector: 'app-business-list',
  templateUrl: './business-list.component.html',
  styleUrls: ['./business-list.component.css']
})
export class BusinessListComponent implements OnInit {

  businesses: Business[];
  pagination: Pagination;
  userParams: any = {};
  isMyBusiness = false;

  constructor(private businessService: BusinessService, private alertify: AlertifyService,
              private route: ActivatedRoute, private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.businesses = data.businesses.result;
      this.pagination = data.businesses.pagination;
    });

    this.userParams.searchQuery = '';
    this.userParams.userId = 0;
  }

  resetFilters() {
    this.userParams.searchQuery = '';
    this.userParams.userId = 0;
    this.loadBusinesses();
  }

  pageChanged(event: any) {
    this.pagination.currentPage = event.page;
    this.loadBusinesses();
  }

  loadBusinesses() {
    if (this.isMyBusiness) {
      this.userParams.userId = this.authService.decodedToken.nameid;
    } else {
      this.userParams.userId = 0;
    }

    this.businessService
      .getBusinesses(this.pagination.currentPage, this.pagination.itemsPerPage, this.userParams)
      .subscribe((res: PaginatedResult<Business[]>) => {
        this.businesses = res.result;
        this.pagination = res.pagination;
      }, error => {
        this.alertify.error(error);
      });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

}
