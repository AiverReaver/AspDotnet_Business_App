import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { RegisterComponent } from './register/register.component';
import { BusinessDetailComponent } from './business/business-detail/business-detail.component';
import { BusinessDetailResolver } from './_resolver/business-detail.resolver';
import { BusinessListComponent } from './business/business-list/business-list.component';
import { BusinessListResolver } from './_resolver/business-list.resolver';
import { BusinessEditComponent } from './business/business-edit/business-edit.component';
import { UserEditComponent } from './user/user-edit/user-edit.component';
import { UserEditResolver } from './_resolver/user-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { BusinessCreateComponent } from './business/business-create/business-create.component';
import { UserBusinessListComponent } from './user/user-business-list/user-business-list.component';
import { UserBusinessListResolver } from './_resolver/user-business-list.resolver';


export const appRoutes: Routes = [
    { path: '', component: HomeComponent, resolve: { businesses: BusinessListResolver} },
    { path: 'register', component: RegisterComponent },
    { path: 'business/:id', component: BusinessDetailComponent,
        resolve: { business: BusinessDetailResolver}},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'messages', component: MessagesComponent},
            { path: 'businesses/list', component: UserBusinessListComponent,
                resolve: { businesses: UserBusinessListResolver }},
            { path: 'business/create', component: BusinessCreateComponent,
                canDeactivate: [PreventUnsavedChanges]},
            { path: 'business/edit/:id', component: BusinessEditComponent,
                resolve: { business: BusinessDetailResolver }, canDeactivate: [PreventUnsavedChanges]},
            { path: 'user/edit', component: UserEditComponent,
                resolve: { user: UserEditResolver}, canDeactivate: [PreventUnsavedChanges]}
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
