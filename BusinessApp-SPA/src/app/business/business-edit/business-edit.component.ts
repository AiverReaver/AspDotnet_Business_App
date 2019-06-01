import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { Business } from 'src/app/_models/Business';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/_services/Auth.service';
import { BusinessService } from 'src/app/_services/business.service';
import { TabsetComponent } from 'ngx-bootstrap';

@Component({
  selector: 'app-business-edit',
  templateUrl: './business-edit.component.html',
  styleUrls: ['./business-edit.component.css']
})
export class BusinessEditComponent implements OnInit {
  @ViewChild('staticTabs') staticTabs: TabsetComponent;
  @ViewChild('editForm') editForm: NgForm;
  business: Business;
  photoUrl: string;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(
    private route: ActivatedRoute,
    private alertify: AlertifyService,
    private businessService: BusinessService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.business = data.business;
    });

    this.route.queryParams.subscribe(params => {
      if (params.isCreated) {
        this.staticTabs.tabs[1].active = true;
      }
    });
    // this.authService.currentPhotoUrl.subscribe(
    //   photoUrl => (this.photoUrl = photoUrl)
    // );
  }

  updateBusiness() {
    this.businessService
      .updateBusiness(this.authService.decodedToken.nameid, this.business)
      .subscribe(
        next => {
          this.alertify.success('Profile updated success');
          this.editForm.reset(this.business);
        },
        error => {
          this.alertify.error(error);
        }
      );
  }

  updateMainPhoto(photoUrl) {
    this.business.photoUrl = photoUrl;
  }

}
