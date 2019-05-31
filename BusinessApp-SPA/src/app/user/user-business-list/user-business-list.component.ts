import { Component, OnInit } from '@angular/core';
import { Business } from 'src/app/_models/Business';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-business-list',
  templateUrl: './user-business-list.component.html',
  styleUrls: ['./user-business-list.component.css']
})
export class UserBusinessListComponent implements OnInit {
  businesses: Business[];
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.businesses = data.businesses;
      console.log(data.businesses);
    });
  }

}
