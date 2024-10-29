import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ApplicationComponent } from './application.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AddComponent } from './demo/add/add.component';
import { ViewComponent } from './demo/view/view.component';
import { ComplaintcategoryComponent } from './complaintcategory/complaintcategory.component';
import { ComplaintcategoryViewComponent } from './complaintcategory-view/complaintcategory-view.component';
import { ComplaintregistrationComponent } from './complaintregistration/complaintregistration.component';
import { ComplaintregistrationupdateComponent } from './complaintregistrationupdate/complaintregistrationupdate.component';
import { ComplaintregistrationdeleteComponent } from './complaintregistrationdelete/complaintregistrationdelete.component';
import {EscalationComponent} from "./escalation/escalation.component"
import {EscalationViewComponent} from "./escalation-view/escalation-view.component";
import { ComplaintCategoryWiseReportsComponent } from './complaint-category-wise-reports/complaint-category-wise-reports.component';
import { DemographymappingComponent } from './demographymapping/demographymapping.component';
import { DemographymappingViewComponent } from './demographymapping-view/demographymapping-view.component';
import { PageComponent } from './page/page.component';
import { PagecontentComponent } from './pagecontent/pagecontent.component';
import { PageViewComponent } from './page-view/page-view.component';
import { PagecontentViewComponent } from './pagecontent-view/pagecontent-view.component';


const routes: Routes = [{
  path: '', component: ApplicationComponent, children: [
    { path: 'dashboard', component: DashboardComponent },
    { path: 'demo', component: AddComponent },
    { path: 'demo/:id', component: ViewComponent },
    { path: 'complaintcategory', component: ComplaintcategoryComponent },
    {path:'complaintcategory-view',component:ComplaintcategoryViewComponent},
    {path:'complaintregistration',component:ComplaintregistrationComponent},
    {path:'complaintregistrationupdate',component:ComplaintregistrationupdateComponent},
    {path:'complaintregistrationdelete',component:ComplaintregistrationdeleteComponent},
    {path:'escalation',component:EscalationComponent},
    {path:'escalation-view',component:EscalationViewComponent},
    {path:'complaint-category-wise-reports',component:ComplaintCategoryWiseReportsComponent},
    {path:'demographymapping',component:DemographymappingComponent},
    {path:'demographymapping-view',component:DemographymappingViewComponent},
    {path:'page',component:PageComponent},
    {path:'pagecontent',component:PagecontentComponent},
    {path:'page-view',component:PageViewComponent},
    {path:'pagecontent-view',component:PagecontentViewComponent},
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ApplicationRoutingModule { }
