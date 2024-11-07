import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  private baseUrl = 'http://localhost:5097/api/Home';

  constructor(private http: HttpClient) { }

  // Get all header menus
  headerNavigationMenus(): Observable<any> {
    return this.http.get(`${this.baseUrl}/getHeaderMenus`);
  }

  //Get all main menus
  mainNavigationMenus(): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetMenuSubmenu`);
  }

  // Get header menu by ID
  getHeaderMenuById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetHeaderMenuById/${id}`);
  }

  // Create or Update header menu
  createOrUpdateHeaderMenu(headerMenu: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/CreateOrUpdateHeaderMenu`, headerMenu, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }

  // Delete header menu by ID
  deleteHeaderMenu(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/DeleteHeaderMenu/${id}`);
  }

  // Get About Us Contents
  aboutUs(): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetAboutUs`);
  }

  // Get How It Works Contents
  howItWorks(): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetHowItWorks`);
  }

  // Get complaint details
  complaintDetails(): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetComplaints`);
  }

  // Get contact details
  contactDetails(): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetContactUsDetails`);
  }
}