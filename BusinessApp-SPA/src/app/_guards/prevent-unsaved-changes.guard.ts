import { Injectable } from '@angular/core';
import { UserEditComponent } from '../user/user-edit/user-edit.component';
import { CanDeactivate } from '@angular/router';
import { BusinessEditComponent } from '../business/business-edit/business-edit.component';

@Injectable()
export class PreventUnsavedChanges implements CanDeactivate<UserEditComponent | BusinessEditComponent> {
    canDeactivate(component: UserEditComponent | BusinessEditComponent) {
        if (component.editForm.dirty) {
            return confirm('Are you sure you want to continue? Any unsaved chnages will be lost');
        }
        return true;
    }
}
