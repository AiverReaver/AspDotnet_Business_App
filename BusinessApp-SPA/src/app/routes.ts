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


export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'business', component: BusinessListComponent,
        resolve: { businesses: BusinessListResolver}},
    { path: 'business/:id', component: BusinessDetailComponent,
        resolve: { business: BusinessDetailResolver}},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'messages', component: MessagesComponent},
            { path: 'business/edit', component: BusinessEditComponent}
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
