import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private districturl = 'https://localhost:7225/api/Dropdown/GetDistricts';  
  private blockurl = 'https://localhost:7225/api/Dropdown/GetBlocks';
  private gpurl = 'https://localhost:7225/api/Dropdown/GetGp';
  private villageurl = 'https://localhost:7225/api/Dropdown/GetVillages';
  private wardurl = 'https://localhost:7225/api/Dropdown/GetWards';
  private registrationApiUrl= 'https://localhost:7024/Api/MANAGE_COMPLAINTDETAILS_CONFIG/DetailcomplaintRegistration';
  private categoryUrl = 'https://localhost:7225/api/Dropdown/GetCategory';
  private subcategoryUrl = 'https://localhost:7225/api/Dropdown/GetSubCategories';
  private fileUploadUrl = 'http://172.27.32.0:8085/api/ComplaintsRegistration/UploadFile';

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

  private handleError(error: any) {
    console.error('An error occurred:', error);
    return throwError(error);
  }
}
