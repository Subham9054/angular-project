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
import { MngbannerViewComponent } from './cms/mngbanner-view/mngbanner-view.component';
import { MinisterProfileViewComponent } from './cms/minister-profile-view/minister-profile-view.component';
import { ComplaintSubCategoryComponent } from './complaint-sub-category/complaint-sub-category.component';
import { ModewiseReportComponent } from './modewise-report/modewise-report.component';
import { ComplaintregistrationViewComponent } from './complaintregistration-view/complaintregistration-view.component';


const routes: Routes = [{
  path: '', component: ApplicationComponent, children: [
    { path: 'dashboard', component: DashboardComponent },
    
    { path: 'demo', component: ViewComponent },
    { path: 'demo/:id', component: AddComponent },
    

    { path: 'complaintcategory/:id', component: ComplaintcategoryComponent },
    { path:'complaintcategory',component:ComplaintcategoryViewComponent},
    
    { path: 'ComplaintSub-Category', component: ComplaintSubCategoryComponent },

    {path:'complaintregistration/:id',component:ComplaintregistrationComponent},
    {path:'complaintregistration',component:ComplaintregistrationViewComponent},

    {path:'complaintregistrationupdate',component:ComplaintregistrationupdateComponent},
    {path:'complaintregistrationdelete',component:ComplaintregistrationdeleteComponent},

    {path:'escalation/:id',component:EscalationComponent},
    {path:'escalation',component:EscalationViewComponent},

    {path:'ModewiseReport',component:ModewiseReportComponent},

    {path:'complaint-category-wise-reports',component:ComplaintCategoryWiseReportsComponent},

    {path:'demographymapping/:id',component:DemographymappingComponent},
    {path:'demographymapping',component:DemographymappingViewComponent},

    {path:'page',component:PageComponent},
    {path:'page-view',component:PageViewComponent},

    {path:'pagecontent/:id',component:PagecontentComponent},
    {path:'pagecontent',component:PagecontentViewComponent},

    {path:'mngbanner/:id',component:MngbannerComponent},
    {path:'mngbanner',component:MngbannerViewComponent},

    {path:'minister-profile/:id',component:MinisterProfileComponent},
    {path:'minister-profile',component:MinisterProfileViewComponent},

    {path:'mngfaq/:id',component:MngfaqComponent},
    {path:'mngfaq',component:MngfaqViewComponent},

    {path:'mnggallery/:id',component:MnggalleryComponent},
    {path:'mnggallery',component:MnggalleryViewComponent},

    
    {path:'mnglogo',component:MnglogoComponent},

   
    {path:'mngmenu',component:MngmenuViewComponent},
    {path:'mngmenu/:id',component:MngmenuComponent},

    {path:'newsevent/:id',component:NewseventComponent},
    {path:'newsevent',component:NewseventViewComponent},

    {path:'whatsnew/:id',component:WhatsnewComponent},
    {path:'whatsnew',component:WhatnewViewComponent},

    {path:'mngcontact/:id',component:MngcontactComponent},
    {path:'mngcontact',component:MngcontactViewComponent},
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ApplicationRoutingModule { }
