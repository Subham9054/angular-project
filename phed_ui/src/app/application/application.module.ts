import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApplicationRoutingModule } from './application-routing.module';
import { ApplicationComponent } from './application.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FooterComponent } from './shared/footer/footer.component';
import { SidebarComponent } from './shared/sidebar/sidebar.component';
import { HeaderComponent } from './shared/header/header.component';
import { AddComponent } from './demo/add/add.component';
import { ViewComponent } from './demo/view/view.component';
import { ComplaintcategoryComponent } from './complaintcategory/complaintcategory.component';
import { ComplaintcategoryViewComponent } from './complaintcategory-view/complaintcategory-view.component';
import { ComplaintregistrationComponent } from './complaintregistration/complaintregistration.component';
import { ComplaintregistrationupdateComponent } from './complaintregistrationupdate/complaintregistrationupdate.component';
import { ComplaintregistrationdeleteComponent } from './complaintregistrationdelete/complaintregistrationdelete.component';
import { EscalationComponent } from './escalation/escalation.component';
import { EscalationViewComponent } from './escalation-view/escalation-view.component';
import { DemographymappingComponent } from './demographymapping/demographymapping.component';
import { DemographymappingViewComponent } from './demographymapping-view/demographymapping-view.component';
import { PageComponent } from './page/page.component';
import { PagecontentComponent } from './pagecontent/pagecontent.component';
import { PageViewComponent } from './page-view/page-view.component';
import { PagecontentViewComponent } from './pagecontent-view/pagecontent-view.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { HighchartsChartModule } from 'highcharts-angular';
import { MnglogoComponent } from './cms/mnglogo/mnglogo.component';
import { MngmenuComponent } from './cms/mngmenu/mngmenu.component';
import { MngbannerComponent } from './cms/mngbanner/mngbanner.component';
import { WhatsnewComponent } from './cms/whatsnew/whatsnew.component';
import { MinisterProfileComponent } from './cms/minister-profile/minister-profile.component';
import { NewseventComponent } from './cms/newsevent/newsevent.component';
import { MnggalleryComponent } from './cms/mnggallery/mnggallery.component';
import { MngfaqComponent } from './cms/mngfaq/mngfaq.component';
import { NewseventViewComponent } from './cms/newsevent-view/newsevent-view.component';
import { MngfaqViewComponent } from './cms/mngfaq-view/mngfaq-view.component';
import { WhatnewViewComponent } from './cms/whatnew-view/whatnew-view.component';
import { MnggalleryViewComponent } from './cms/mnggallery-view/mnggallery-view.component';
import { MngmenuViewComponent } from './cms/mngmenu-view/mngmenu-view.component';
import { MngcontactComponent } from './cms/mngcontact/mngcontact.component';
import { MngcontactViewComponent } from './cms/mngcontact-view/mngcontact-view.component';
import { MngbannerViewComponent } from './cms/mngbanner-view/mngbanner-view.component';
import { MinisterProfileViewComponent } from './cms/minister-profile-view/minister-profile-view.component';

@NgModule({
  declarations: [
    ApplicationComponent,
    DashboardComponent,
    FooterComponent,
    SidebarComponent,
    HeaderComponent,
    AddComponent,
    ViewComponent,
    ComplaintcategoryComponent,
    ComplaintcategoryViewComponent,
    ComplaintregistrationComponent,
    ComplaintregistrationupdateComponent,
    ComplaintregistrationdeleteComponent,
    EscalationComponent,
    EscalationViewComponent,
    DemographymappingComponent,
    DemographymappingViewComponent,
    PageComponent,
    PagecontentComponent,
    PageViewComponent,
    PagecontentViewComponent,
    MnglogoComponent,
    MngmenuComponent,
    MngbannerComponent,
    WhatsnewComponent,
    MinisterProfileComponent,
    NewseventComponent,
    MnggalleryComponent,
    MngfaqComponent,
    NewseventViewComponent,
    MngfaqViewComponent,
    WhatnewViewComponent,
    MnggalleryViewComponent,
    MngmenuViewComponent,
    MngcontactComponent,
    MngcontactViewComponent,
    MngbannerViewComponent,
    MinisterProfileViewComponent
  ],
  imports: [
    CommonModule,
    ApplicationRoutingModule,
    FormsModule,
    CKEditorModule,
    HighchartsChartModule
  ],
  exports: [ 
    ApplicationComponent,
    DashboardComponent,
    FooterComponent,
    SidebarComponent,
    HeaderComponent,
    AddComponent,
    ViewComponent,
    ComplaintcategoryComponent // Export the ComplaintcategoryComponent
  ]
})
export class ApplicationModule { }
