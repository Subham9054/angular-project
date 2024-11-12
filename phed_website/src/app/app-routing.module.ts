import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
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
import { GallerydetailsComponent } from './gallerydetails/gallerydetails.component';
import { SitemapComponent } from './sitemap/sitemap.component';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';
import { GrievancestatusComponent } from './grievancestatus/grievancestatus.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'home'},
  { path: 'home', component: HomeComponent },
  { path: 'about-us', component: AboutusComponent },
  { path: 'faq', component: FaqComponent },
  { path: 'privacy-policy', component: PrivacyPolicyComponent },
  { path: 'contact-us', component: ContactUsComponent },
  { path: 'how-it-works', component: HowitworksComponent },
  { path: 'complaint', component: ComplaintComponent },
  { path: 'events', component: EventsComponent },
  { path: 'whats-new', component: WhatsnewComponent },
  { path: 'complaintregistration', component: RegistrationcomplaintComponent },
  { path: 'gallery', component: GalleryComponent },
  { path: 'event-details', component: DetaileventComponent },
  { path: 'gallery-details', component: GallerydetailsComponent },
  { path: 'site-map', component: SitemapComponent },
  { path: 'grievance-status', component: GrievancestatusComponent },
  { path: '**', component: PagenotfoundComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { scrollPositionRestoration: 'top' }) 
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
