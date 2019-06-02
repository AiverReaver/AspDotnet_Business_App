import { Component, OnInit } from '@angular/core';
import { Business } from 'src/app/_models/Business';
import { BusinessService } from 'src/app/_services/business.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';


@Component({
  selector: 'app-business-detail',
  templateUrl: './business-detail.component.html',
  styleUrls: ['./business-detail.component.css']
})
export class BusinessDetailComponent implements OnInit {

  business: Business;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  cld: any;

  constructor(private businessService: BusinessService, private alertify: AlertifyService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.business = data.business;
    });

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Rotate,
        imageAutoPlay: true,
        imageAutoPlayInterval: 6000,
        imageAutoPlayPauseOnHover: true,
        preview: true
      },
      {
        breakpoint: 800,
        imageAutoPlay: false,
        width : '100%'
      }
    ];

    this.galleryImages = this.getImages();
  }

  getImages() {
    const imageUrls = [];
    for (const photo of this.business.photos) {
      imageUrls.push({
        small: photo.url,
        medium: photo.url,
        big: photo.url,
        description: photo.description
      });
    }
    return imageUrls;
  }

}
