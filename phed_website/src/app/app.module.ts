import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
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
import { NgSelectModule } from '@ng-select/ng-select'; // Import NgSelectModule
import { GrievancestatusComponent } from './grievancestatus/grievancestatus.component';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';

// Factory function to create the TranslateHttpLoader
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, '/assets/i18n/', '.json');
}

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
    HttpClientModule,
    NgSelectModule, // Add NgSelectModule here
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
