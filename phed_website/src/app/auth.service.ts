import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private districturl = 'http://192.168.10.47:5009/gateway/GetDistricts';  
  private blockurl = 'http://192.168.10.47:5009/gateway/GetBlocks';
  private gpurl = 'http://192.168.10.47:5009/gateway/GetGp';
  private villageurl = 'http://192.168.10.47:5009/gateway/GetVillages';
  private wardurl = '192.168.10.47:5006/api/Dropdown/GetWards';
  private registrationApiUrl= 'http://192.168.10.47:5009/gateway/DetailcomplaintRegistration';
  private categoryUrl = 'http://192.168.10.47:5009/gateway/GetCategory';
  private subcategoryUrl = 'http://192.168.10.47:5009/gateway/GetSubCategories';
  private fileUploadUrl = 'http://192.168.10.47:5009/gateway/UploadFile';


  private faqUrl = 'http://192.168.10.47:5006/api/FAQ/GetFaqs'; //Base URL for Getting FAQs

  constructor(private http: HttpClient, private router: Router) {}

  getCategories(): Observable<any> {
    return this.http.get<any>(this.categoryUrl).pipe(
      catchError(this.handleError)
    );
  }

  getSubcategories(catid: number): Observable<any> {
    return this.http.get<any>(`${this.subcategoryUrl}?catid=${catid}`).pipe(
      catchError(this.handleError)
    );
  }

  getDistricts(): Observable<any> {
    return this.http.get<any>(this.districturl).pipe(
      catchError(this.handleError)
    );
  }

  getBlocks(distId: number): Observable<any> {
    return this.http.get<any>(`${this.blockurl}?distid=${distId}`).pipe(
      catchError(this.handleError)
    );
  }

  getGps( blockId: number): Observable<any> {
    return this.http.get<any[]>(`${this.gpurl}?blockid=${blockId}`).pipe(
      catchError(this.handleError)
    );
  }

  getVillages( gpId: number): Observable<any> {
    return this.http.get<any>(`${this.villageurl}?gpid=${gpId}`).pipe(
      catchError(this.handleError)
    );
  }
  
  getWards( villageid: number): Observable<any> {
    return this.http.get<any>(`${this.wardurl}?villageid=${villageid}`).pipe(
      catchError(this.handleError)
    );
  }
  submitRegistration(registrationData: any): Observable<any> {
    return this.http.post(this.registrationApiUrl, registrationData, {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      responseType: 'text' // Assuming the API returns text response, change this if necessary
    }).pipe(
      catchError(this.handleError) // Proper error handling
    );
}


  uploadFile(file: File): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post<{ message: string; fileName: string }>(this.fileUploadUrl, formData).pipe(
      catchError(this.handleError)
    );
  }

  //**********Website Dynamic Work Methods by Debasis Das**********
  getFAQs(): Observable<any> {
    return this.http.get(this.faqUrl).pipe(
      catchError(this.handleError)
    );
  }

  // Error handling logic
  private handleError(error: any) {
    console.error('An error occurred:', error);
    return throwError(() => new Error(error.message || 'Unknown error occurred'));
  }
}
