import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { Photo } from '../../_models/Photo';
import { environment } from 'src/environments/environment';
import { AuthService } from '../../_services/Auth.service';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { BusinessService } from 'src/app/_services/business.service';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() businessId: number;
  @Input() photos: Photo[];
  @Output() getMemberPhotoChange = new EventEmitter<string>();
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  currentMain: Photo;

  constructor(
    private authService: AuthService,
    private businessService: BusinessService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.initializeUploader();
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url:
        this.baseUrl +
        'businesses/' +
        this.businessId +
        '/photos',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image', 'video', 'document'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onBeforeUploadItem = file => {
      if (file.file.type.startsWith('image')) {
        file.url = this.baseUrl + 'businesses/' + this.businessId + '/photos';
      } else if (file.file.type.startsWith('video')) {
        file.url = this.baseUrl + 'businesses/' + this.businessId + '/videos';
      } else {
        this.alertify.warning('Only Video or Image are supported');
      }
    };

    this.uploader.onAfterAddingFile = file => {
      file.withCredentials = false;
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (item.file.type.startsWith('image') && response) {
        const res: Photo = JSON.parse(response);
        const photo = {
          id: res.id,
          url: res.url,
          dateAdded: res.dateAdded,
          description: res.description,
          isMain: res.isMain
        };
        this.photos.push(photo);
      }
    };
  }

  setMainPhoto(photo: Photo) {
    this.businessService
      .setMainPhoto(this.businessId, photo.id)
      .subscribe(
        () => {
          this.currentMain = this.photos.filter(p => p.isMain === true)[0];
          this.currentMain.isMain = false;
          photo.isMain = true;
          // this.authService.changeMemberPhoto(photo.url);
          // this.authService.currentUser.photoUrl = photo.url;
          // localStorage.setItem(
          //   'user',
          //   JSON.stringify(this.authService.currentUser)
          // );
          this.getMemberPhotoChange.emit(photo.url);
        },
        error => {
          this.alertify.error(error);
        }
      );
  }

  deletePhoto(id: number) {
    this.alertify.confirm('Are you sure you want to delete this photo?', () => {
      this.businessService
        .deletePhoto(this.businessId, id)
        .subscribe(
          () => {
            this.photos.splice(this.photos.findIndex(p => p.id === id), 1);
            this.alertify.success('Photo has been deleted');
          },
          error => {
            this.alertify.error('failed to delete the photo');
          }
        );
    });
  }
}
