import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router'; 
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private secretKey: string = 'my-secret-key'; 
  private token: string | null = null; // Store the token

  private baseUrl: string = 'http://localhost:8085/api';
  

  private apiUrl = 'http://172.27.32.0:8085/api/Login'; // Your API URL for login
  private registrationApiUrl = 'http://172.27.32.0:8085/api/ComplaintsRegistration/DetailcomplaintRegistration';
  private complaintApiUrl = 'http://172.27.32.0:8085/api/Complaint/ComplaintCategory';
  private getComplaintApiUrl = 'http://172.27.32.0:8085/api/Complaint/GetallComplaint';
  private updateComplaintApiUrl = 'http://172.27.32.0:8085/api/Complaint/UpdateComplaint';
  private getComplaintByIdApiUrl = 'http://172.27.32.0:8085/api/Complaint/GetComplaintById';
  private deleteapiurl = 'http://172.27.32.0:8085/api/Complaint/deleteComplaintbyid';
  private districturl = 'https://localhost:7225/api/Dropdown/GetDistricts';  
  private blockurl = 'https://localhost:7225/api/Dropdown/GetBlocks';
  private gpurl = 'https://localhost:7225/api/Dropdown/GetGp';
  private villageurl = 'https://localhost:7225/api/Dropdown/GetVillages';
  private categoryUrl = 'https://localhost:7225/api/Dropdown/GetCategory';
  private subcategoryUrl = 'https://localhost:7225/api/Dropdown/GetSubCategories';
  private fileUploadUrl = 'http://172.27.32.0:8085/api/ComplaintsRegistration/UploadFile';
  private complaintstatusUrl = 'http://172.27.32.0:8085/api/ComplaintsRegistration/GetComplaints';
  private complainttypeurl = 'https://localhost:7225/api/Dropdown/GetComplaintstype';
  private getdesignationurl='https://localhost:7225/api/Dropdown/GetDesignation';
  private getloclevel='https://localhost:7225/api/Dropdown/GetLocationLevel';
 
  constructor(private http: HttpClient, private router: Router) { }
 
  // Method for user login
  login(loginPayload: { vchUserName: string, vchPassWord: string }): Observable<any> {
    const loginUrl = this.apiUrl; // Get the full URL (decrypted base + endpoint)
    console.log('Login URL:', loginUrl); // Log the full URL being used for the API call
    return this.http.post(loginUrl, loginPayload).pipe(
      catchError(this.handleError)
    );
  }
  // Save the token (call this after successful login)
  saveToken(token: string): void {
    this.token = token;
    sessionStorage.setItem('token', token);  // Save token in session storage
  }

  // Method to get the token
  getToken(): string | null {
    return this.token || sessionStorage.getItem('token');  // Retrieve token from session storage
  }

  // Method to check if token is expired
  isTokenExpired(): boolean {
    const expiryTime = sessionStorage.getItem('tokenExpiry');
    if (expiryTime) {
      return new Date().getTime() > parseInt(expiryTime, 10);
    }
    return true;
  }

  // Method to handle logout
  logout() {
    sessionStorage.clear();  // Clears all session data
  }

  getCategories(): Observable<any> {
    return this.http.get<any>(this.categoryUrl).pipe(
      catchError(this.handleError)
    );
  }
  getcomplainttype():Observable<any>{
    return this.http.get<any>(this.complainttypeurl).pipe(
      catchError(this.handleError)
    )
  }
  getComplaintStatus(): Observable<any> {
    return this.http.get<any>(this.complaintstatusUrl).pipe(
      catchError(this.handleError)
    );
  }

  getComplaintType(): Observable<any> {
    return this.http.get<any>(this.complainttypeurl).pipe(
      catchError(this.handleError)
    );
  }

  getSubcategories(catid: number): Observable<any> {
    return this.http.get<any>(`${this.subcategoryUrl}?catid=${catid}`).pipe(
      catchError(this.handleError)
    );
  }

  // Get districts, blocks, GPS, villages
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

  getGps(distId: number, blockId: number): Observable<any> {
    return this.http.get<any[]>(`${this.gpurl}?distid=${distId}&blockid=${blockId}`).pipe(
      catchError(this.handleError)
    );
  }

  getVillages(distId: number, blockId: number, gpId: number): Observable<any> {
    return this.http.get<any>(`${this.villageurl}?distid=${distId}&blockid=${blockId}&gpid=${gpId}`).pipe(
      catchError(this.handleError)
    );
  }

  // Registration data submission
  submitRegistration(registrationData: any): Observable<any> {
    return this.http.post(this.registrationApiUrl, registrationData, {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      responseType: 'text' // Set response type to text
    }).pipe(
      catchError(this.handleError)
    );
  }

  // File upload method
  uploadFile(file: File): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post<{ message: string; fileName: string }>(this.fileUploadUrl, formData).pipe(
      catchError(this.handleError)
    );
  }
  getcomplaintstatus():Observable<any>{
    return this.http.get<any>(this.complaintstatusUrl).pipe(
      catchError(this.handleError)
    )
  }
 
  submitComplaint(complaintData: { VCH_CATEGORY: string, NVCH_CATEGORY: string }): Observable<any> {
    return this.http.post(this.complaintApiUrl, complaintData)
      .pipe(catchError(this.handleError));
  }

  getAllComplaints(): Observable<any> {
    return this.http.get(this.getComplaintApiUrl).pipe(
      catchError(this.handleError)
    );
  }

  updateComplaint(id: number, complaintData: { VCH_CATEGORY: string, NVCH_CATEGORY: string }): Observable<any> {
    return this.http.post(`${this.updateComplaintApiUrl}/${id}`, complaintData)
      .pipe(catchError(this.handleError));
  }

  delete(id: number): Observable<any> {
    const url = `${this.deleteapiurl}/${id}`;  // Use the delete API URL with ID
    return this.http.delete(url).pipe(
      catchError(this.handleError)
    );
  }
  getsubcategories(catid: number): Observable<any> {
    return this.http.get<any>(`${this.subcategoryUrl}?catid=${catid}`).pipe(
      catchError(this.handleError)
    );
  }

  getComplaintById(id: number): Observable<any> {
    return this.http.get(`${this.getComplaintByIdApiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }
  getDesignation():Observable<any>{
    return this.http.get(this.getdesignationurl).pipe(
      catchError(this.handleError)
    );
  }

  getLocation():Observable<any>{
    return this.http.get(this.getloclevel).pipe(
      catchError(this.handleError)
    );
  }
  // Error handling logic
  private handleError(error: any) {
    console.error('An error occurred:', error);
    return throwError(error);
  }
}
