import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './shared/header/header.component';
import { FooterComponent } from './shared/footer/footer.component';
import { AboutusComponent } from './aboutus/aboutus.component';
import { FaqComponent } from './faq/faq.component';
import { PrivacyPolicyComponent } from './privacy-policy/privacy-policy.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { HowitworksComponent } from './howitworks/howitworks.component';
import { ComplaintComponent } from './complaint/complaint.component';
import { EventsComponent } from './events/events.component';
import { WhatsnewComponent } from './whatsnew/whatsnew.component';
import { RegistrationcomplaintComponent } from './registrationcomplaint/registrationcomplaint.component';
import { GalleryComponent } from './gallery/gallery.component';
import { DetaileventComponent } from './detailevent/detailevent.component';
import { HomeComponent } from './home/home.component';
import { GallerydetailsComponent } from './gallerydetails/gallerydetails.component';
import { SitemapComponent } from './sitemap/sitemap.component';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { GrievancestatusComponent } from './grievancestatus/grievancestatus.component';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';
import { HttpClientModule } from '@angular/common/http'; 

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    FooterComponent,
    AboutusComponent,
    FaqComponent,
    PrivacyPolicyComponent,
    ContactUsComponent,
    HowitworksComponent,
    ComplaintComponent,
    EventsComponent,
    WhatsnewComponent,
    RegistrationcomplaintComponent,
    GalleryComponent,
    DetaileventComponent,
    GallerydetailsComponent,
    SitemapComponent,
    GrievancestatusComponent,
    PagenotfoundComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgxDropzoneModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
