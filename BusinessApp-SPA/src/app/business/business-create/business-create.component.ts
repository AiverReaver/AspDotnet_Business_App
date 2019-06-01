import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Business } from 'src/app/_models/Business';
import { BusinessService } from 'src/app/_services/business.service';

@Component({
  selector: 'app-business-create',
  templateUrl: './business-create.component.html',
  styleUrls: ['./business-create.component.css']
})
export class BusinessCreateComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter();
  business: Business;
  registerForm: FormGroup;

  constructor(private businessService: BusinessService, private router: Router,
              private alertify: AlertifyService, private fb: FormBuilder) { }


  ngOnInit() {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      name: ['', Validators.required],
      address: ['', Validators.required],
      landmark: ['', Validators.required],
      officeNumber: ['', [Validators.required, Validators.minLength(10), Validators.pattern('[0-9]+')]],
      description: ['', Validators.required]
    });
  }

  register() {
    if (this.registerForm.valid) {
      this.business = Object.assign({}, this.registerForm.value);
      this.businessService.createBusiness(this.business).subscribe((res) => {
        this.alertify.success('Your business is created');
        this.business = res;
      }, error => {
        this.alertify.error(error);
      }, () => {
        this.router.navigate(['/businesses/edit/' + this.business.id], { queryParams: { isCreated: true} });
      });
    }
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
