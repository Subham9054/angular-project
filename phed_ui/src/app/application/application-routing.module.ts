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
import { MinisterProfileComponent } from './cms/minister-profile/minister-profile.component';
import { MngbannerComponent } from './cms/mngbanner/mngbanner.component';
import { MngfaqComponent } from './cms/mngfaq/mngfaq.component';
import { MnggalleryComponent } from './cms/mnggallery/mnggallery.component';
import { MnglogoComponent } from './cms/mnglogo/mnglogo.component';
import { MngmenuComponent } from './cms/mngmenu/mngmenu.component';
import { NewseventComponent } from './cms/newsevent/newsevent.component';
import { WhatsnewComponent } from './cms/whatsnew/whatsnew.component';
import { NewseventViewComponent } from './cms/newsevent-view/newsevent-view.component';
import { MngfaqViewComponent } from './cms/mngfaq-view/mngfaq-view.component';
import { WhatnewViewComponent } from './cms/whatnew-view/whatnew-view.component';
import { MnggalleryViewComponent } from './cms/mnggallery-view/mnggallery-view.component';
import { MngmenuViewComponent } from './cms/mngmenu-view/mngmenu-view.component';
import { MngcontactComponent } from './cms/mngcontact/mngcontact.component';
import { MngcontactViewComponent } from './cms/mngcontact-view/mngcontact-view.component';


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
    {path:'page-view',component:PageViewComponent},

    {path:'pagecontent',component:PagecontentComponent},
    {path:'pagecontent-view',component:PagecontentViewComponent},

    {path:'minister-profile',component:MinisterProfileComponent},

    {path:'mngbanner',component:MngbannerComponent},

    {path:'mngfaq',component:MngfaqComponent},
    {path:'mngfaq-view',component:MngfaqViewComponent},
    
    {path:'mnggallery',component:MnggalleryComponent},
    {path:'mnggallery-view',component:MnggalleryViewComponent},

    {path:'mnglogo',component:MnglogoComponent},

    {path:'mngmenu',component:MngmenuComponent},
    {path:'mngmenu-view',component:MngmenuViewComponent},

    {path:'newsevent',component:NewseventComponent},
    {path:'newsevent-view',component:NewseventViewComponent},

    {path:'whatsnew',component:WhatsnewComponent},
    {path:'whatnew-view',component:WhatnewViewComponent},

    {path:'mngcontact',component:MngcontactComponent},
    {path:'mngcontact-view',component:MngcontactViewComponent},
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ApplicationRoutingModule { }
