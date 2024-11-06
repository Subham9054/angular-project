import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ComplaintcategoryComponent } from './application/complaintcategory/complaintcategory.component';
import { ComplaintcategoryViewComponent } from './application/complaintcategory-view/complaintcategory-view.component';
import { ComplaintregistrationComponent } from './application/complaintregistration/complaintregistration.component';
import { AuthGuard } from './auth.guard';
import { ApplicationComponent } from './application/application.component';
import { ForgotpasswordComponent } from './forgotpassword/forgotpassword.component';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';


const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'application', component: ApplicationComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent, pathMatch: 'full' },
  { path: 'forgotpassword', component: ForgotpasswordComponent },
  { path: 'complaint-category', component: ComplaintcategoryComponent },
  { path: 'complaintcategory-view', component: ComplaintcategoryViewComponent }, 
  { path: 'complaint-registration', component: ComplaintregistrationComponent },
  {
    path: 'application',
    loadChildren: () => import('./application/application.module').then(m => m.ApplicationModule)
  },
  { path: '**', component: PagenotfoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
