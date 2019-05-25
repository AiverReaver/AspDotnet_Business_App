import { Injectable } from '@angular/core';
import { UserEditComponent } from '../user-edit/user-edit.component';
import { CanDeactivate } from '@angular/router';

@Injectable()
export class PreventUnsavedChanges implements CanDeactivate<UserEditComponent> {
    canDeactivate(component: UserEditComponent) {
        if (component.editform.dirty) {
            return confirm('Are you sure you want to continue? Any unsaved chnages will be lost');
        }
        return true;
    }
}
