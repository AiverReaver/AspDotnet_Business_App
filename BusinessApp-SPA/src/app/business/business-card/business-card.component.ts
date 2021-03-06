import { Component, OnInit, Input } from '@angular/core';
import { Business } from 'src/app/_models/Business';

@Component({
  selector: 'app-business-card',
  templateUrl: './business-card.component.html',
  styleUrls: ['./business-card.component.css']
})
export class BusinessCardComponent implements OnInit {
  @Input() business: Business;
  @Input() routeUrl: string;

  constructor() { }

  ngOnInit() {
  }

}
