import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router'; 

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private token: string | null = null; // Store the token

  // Define API URLs
  private apiUrl = 'https://localhost:44303/api/Login'; // Your API URL for login
  private registrationApiUrl = 'https://localhost:44303/api/ComplaintsRegistration/DetailcomplaintRegistration';
  private complaintApiUrl = 'https://localhost:44303/api/Complaint/ComplaintCategory';
  private getComplaintApiUrl = 'https://localhost:44303/api/Complaint/GetallComplaint';
  private updateComplaintApiUrl = 'https://localhost:44303/api/Complaint/UpdateComplaint';
  private getComplaintByIdApiUrl = 'https://localhost:44303/api/Complaint/GetComplaintById';
  private deleteapiurl = 'https://localhost:44303/api/Complaint/deleteComplaintbyid';
  private districturl = 'https://localhost:44303/api/ComplaintsRegistration/GetDistricts';
  private blockurl = 'https://localhost:44303/api/ComplaintsRegistration/GetBlocks';
  private gpurl = 'https://localhost:44303/api/ComplaintsRegistration/GetGp';
  private villageurl = 'https://localhost:44303/api/ComplaintsRegistration/GetVillages';
  private categoryUrl = 'https://localhost:44303/api/ComplaintsRegistration/GetCategory';
  private subcategoryUrl = 'https://localhost:44303/api/ComplaintsRegistration/GetSubCategories';
  private fileUploadUrl = 'https://localhost:44303/api/ComplaintsRegistration/UploadFile';
  private complaintstatusUrl = 'https://localhost:44303/api/ComplaintsRegistration/GetComplaints';
  private complainttypeurl = 'https://localhost:44303/api/ComplaintsRegistration/GetComplaintstype';

  constructor(private http: HttpClient, private router: Router) { }  // Inject Router

  // Method for user login
  login(loginPayload: { vchUserName: string, vchPassWord: string, Role: string }): Observable<any> {
    return this.http.post(this.apiUrl, loginPayload).pipe(
      catchError(this.handleError)  // Error handling method
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

  // Other methods...

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
  // Complaint submission
  submitComplaint(complaintData: { VCH_CATEGORY: string, NVCH_CATEGORY: string }): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.getToken()}`  // Include the token in the headers
    });

    return this.http.post(this.complaintApiUrl, complaintData, { headers })
      .pipe(catchError(this.handleError));
  }

  getAllComplaints(): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.getToken()}`  // Add the token to the headers
    });
  
    return this.http.get(this.getComplaintApiUrl, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  updateComplaint(id: number, complaintData: { VCH_CATEGORY: string, NVCH_CATEGORY: string }): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.getToken()}`  // Include the token in the headers
    });

    return this.http.post(`${this.updateComplaintApiUrl}/${id}`, complaintData, { headers })
      .pipe(catchError(this.handleError));
  }

  delete(id: number): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.getToken()}`  // Ensure this line retrieves the token
    });
  
    const url = `${this.deleteapiurl}/${id}`;  // Use the delete API URL with ID
    return this.http.delete(url, { headers }).pipe(
      catchError(this.handleError)
    );
  }
  getsubcategories(catid: number): Observable<any> {
    return this.http.get<any>(`${this.subcategoryUrl}?catid=${catid}`).pipe(
      catchError(this.handleError)
    );
  }
  getComplaintById(id: number): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.getToken()}`  // Include the token in the headers
    });

    return this.http.get(`${this.getComplaintByIdApiUrl}/${id}`, { headers })
      .pipe(catchError(this.handleError));
  }

  // Error handling logic
  private handleError(error: any) {
    console.error('An error occurred:', error);
    return throwError(error);
  }
}
