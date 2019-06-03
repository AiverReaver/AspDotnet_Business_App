import { Component, OnInit, ViewChild, ElementRef, AfterViewChecked} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PaymentService } from '../_services/payment.service';

@Component({
  selector: 'app-paytm-payment',
  templateUrl: './paytm-payment.component.html',
  styleUrls: ['./paytm-payment.component.css']
})
export class PaytmPaymentComponent implements OnInit, AfterViewChecked {

  paytmParams: any = {};
  @ViewChild('paymentForm') myFormPost: ElementRef;

  constructor(private route: ActivatedRoute, private payment: PaymentService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.paytmParams = data.paytmParams;
    });
    console.log(this.paytmParams);
  }

  ngAfterViewChecked() {
    this.myFormPost.nativeElement.submit();
  }

}
