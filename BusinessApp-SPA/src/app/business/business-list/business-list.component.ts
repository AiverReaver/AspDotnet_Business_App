import { Component, OnInit } from '@angular/core';
import { Business } from '../../_models/Business';
import { BusinessService } from '../../_services/business.service';
import { AlertifyService } from '../../_services/alertify.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-business-list',
  templateUrl: './business-list.component.html',
  styleUrls: ['./business-list.component.css']
})
export class BusinessListComponent implements OnInit {

  businesses: Business[];

  constructor(private businessService: BusinessService, private alertify: AlertifyService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.businesses = data.businesses;
    });
  }

}
