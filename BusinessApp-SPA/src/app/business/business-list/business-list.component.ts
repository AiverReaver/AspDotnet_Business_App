import { Component, OnInit } from '@angular/core';
import { Business } from '../../_models/Business';
import { BusinessService } from '../../_services/business.service';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-business-list',
  templateUrl: './business-list.component.html',
  styleUrls: ['./business-list.component.css']
})
export class BusinessListComponent implements OnInit {

  businesses: Business[];

  constructor(private businessService: BusinessService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.loadBusinesses();
  }

  loadBusinesses() {
    this.businessService.getBusinesses().subscribe((businesses: Business[]) => {
      this.businesses = businesses;
    }, error => {
      this.alertify.error(error);
    });
  }

}
