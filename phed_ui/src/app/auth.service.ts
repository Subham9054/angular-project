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
  

  private apiUrl = 'https://localhost:7199/Login'; // Your API URL for login
  private registrationApiUrl = 'https://localhost:7024/Api/MANAGE_COMPLAINTDETAILS_CONFIG/DetailcomplaintRegistration';
  private complaintApiUrl = 'https://localhost:7010/Api/MANAGE_CATEGORYMASTER/ComplaintCategory';
  private getComplaintApiUrl = 'https://localhost:7010/Api/MANAGE_CATEGORYMASTER/GetallComplaint';
  private updateComplaintApiUrl = 'https://localhost:7010/Api/MANAGE_CATEGORYMASTER/UpdateComplaint';
  private getComplaintByIdApiUrl = 'http://172.27.32.0:8085/api/Complaint/GetComplaintById';
  private deleteapiurl = 'https://localhost:7010/Api/MANAGE_CATEGORYMASTER/deleteComplaintbyid';
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
  private insertescalationurl="https://localhost:7237/Api/MANAGE_ESCALATION_CONFIGDETAILS/insertescalation";
  private checkApiUrl = 'https://localhost:7237/Api/MANAGE_ESCALATION_CONFIGDETAILS/check';
  private viewEscalationurl='https://localhost:7237/Api/MANAGE_ESCALATION_CONFIGDETAILS/viewescalation';
  private viewEscalationurleye= 'https://localhost:7237/Api/MANAGE_ESCALATION_CONFIGDETAILS/viewescalationeye';
  private viewupdatepenurl='https://localhost:7237/Api/MANAGE_ESCALATION_CONFIGDETAILS/viewupdatepen';
  private getPrioritiesUrl='https://localhost:7225/api/Dropdown/GetComplaintPriority';
  private submitsubcaturl='https://localhost:7010/Api/MANAGE_SUBCATEGORYMASTER/ComplaintSubCategory'
  private getallsubcaturl = 'https://localhost:7010/Api/MANAGE_SUBCATEGORYMASTER/ViewComplaintSubCategory';
  private updatesubcaturl='https://localhost:7010/Api/MANAGE_SUBCATEGORYMASTER/UpdateComplaintSubCategory';
  private deletesubcaturl= 'https://localhost:7010/Api/MANAGE_SUBCATEGORYMASTER/DeleteSubcat';

  //For Content Management URLs
  private getParentMenusUrl = 'http://localhost:5097/api/CMS/GetParentMenus';
  private createOrUpdatePageUrl = 'http://localhost:5097/api/CMS/CreateOrUpdatePageLink';
  private getAllPageLinksUrl = 'http://localhost:5097/api/CMS/GetPageLinks';
  private getPageLinkByIdUrl = 'http://localhost:5097/api/CMS/GetPageLinkById';
  private deletePageLinkUrl = 'http://localhost:5097/api/CMS/DeletePageLink';
  private getMenuSubmenuUrl = 'http://localhost:5097/api/CMS/GetMenuSubmenu';

  
  private createOrUpdateBannerUrl = 'http://localhost:5097/api/CMS/CreateOrUpdateBanner';
  private getAllBannersUrl = 'http://localhost:5097/api/CMS/GetBanners';
  private getBannerByIdUrl = 'http://localhost:5097/api/CMS/GetBannerById';
  private getBannerByNameUrl = 'http://localhost:5097/api/CMS/GetBannerByName';
  private deleteBannerUrl = 'http://localhost:5097/api/CMS/DeleteBanner';
  
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

  submitEscalationData(data: any): Observable<any> {
    // Ensure the URL is correctly defined
    return this.http.post(`${this.insertescalationurl}`, data);
  }

  checkEscalation(INT_CATEGORY_ID: string, INT_SUB_CATEGORY_ID: string): Observable<any> {
    // Make HTTP request with proper API URL and query parameters
    return this.http.get<any>(`${this.checkApiUrl}?categoryId=${INT_CATEGORY_ID}&subcategoryId=${INT_SUB_CATEGORY_ID}`).pipe(
        catchError(this.handleError)
    );
  }

  viewEscalation(INT_CATEGORY_ID: string, INT_SUB_CATEGORY_ID: string): Observable<any> {
    // Make HTTP request with proper API URL and query parameters
    return this.http.get<any>(`${this.viewEscalationurl}?categoryId=${INT_CATEGORY_ID}&subcategoryId=${INT_SUB_CATEGORY_ID}`).pipe(
        catchError(this.handleError)
    );
  }
  viewEscalationeye(INT_CATEGORY_ID: string, INT_SUB_CATEGORY_ID: string): Observable<any> {
    // Make HTTP request with proper API URL and query parameters
    return this.http.get<any>(`${this.viewEscalationurleye}?categoryId=${INT_CATEGORY_ID}&subcategoryId=${INT_SUB_CATEGORY_ID}`).pipe(
        catchError(this.handleError)
    );
  }
  UpdateEscalation(INT_CATEGORY_ID: string, INT_SUB_CATEGORY_ID: string, INT_ESCALATION_LEVELID: string): Observable<any> {
    return this.http.get<any>(`${this.viewupdatepenurl}?categoryId=${INT_CATEGORY_ID}&subcategoryId=${INT_SUB_CATEGORY_ID}&esclid=${INT_ESCALATION_LEVELID}`).pipe(
      catchError(this.handleError)
    );
  }
  getPriorities(): Observable<any> {
    return this.http.get<any>(this.getPrioritiesUrl).pipe(
      catchError(this.handleError)
    );
  }



  submitSubcategory(registrationData: any): Observable<any> {
    return this.http.post(`${this.submitsubcaturl}`, registrationData).pipe(
      catchError(error => {
        console.error('Error submitting registration data:', error);
        return throwError(error); // propagate the error
      })
    );
  }
  

 
  
  getComplaintSubCategory(INT_CATEGORY_ID: string, INT_SUB_CATEGORY_ID: string): Observable<any> {
    return this.http.get<any>(`${this.getallsubcaturl}?catid=${INT_CATEGORY_ID}&subcatid=${INT_SUB_CATEGORY_ID}`).pipe(
      catchError(this.handleError)  // Corrected missing parenthesis here
    );
  }

  UpdateSubCategory(INT_CATEGORY_ID: string, INT_SUB_CATEGORY_ID: string): Observable<any> {
    return this.http.get<any>(`${this.getallsubcaturl}?catid=${INT_CATEGORY_ID}&subcatid=${INT_SUB_CATEGORY_ID}`).pipe(
      catchError(this.handleError)
    );
  }

  updateSubCat(subcatid: string, registrationData: any) {
    return this.http.put<any>(`${this.updatesubcaturl}?subcatid=${subcatid}`, registrationData).pipe(
      catchError(this.handleError)
    );
  }
  
  deleteComplaintSubCategory(catid: string, subcatid: string): Observable<any> {
    return this.http.delete<any>(`${this.deletesubcaturl}?catid=${catid}&subcatid=${subcatid}`).pipe(
      catchError(this.handleError)  // Corrected missing parenthesis here
    );
  }
  
  //******.....Methods For Content Managent Dynamic Work by Debasis Das.....******
  GetParentMenus(): Observable<any> {
    return this.http.get(this.getParentMenusUrl).pipe(
      catchError(this.handleError)
    );
  }

  // Method to send form data to the updated URL
  CreateOrUpdatePageLink(formData: FormData, pageId?: number): Observable<any> {
    const headers = new HttpHeaders();
    // Construct the URL based on the presence of pageId
    const url = pageId ? `${this.createOrUpdatePageUrl}` : this.createOrUpdatePageUrl;
    // Use POST for both creating and updating
    return this.http.post(url, formData, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  DeletePageLink(pageId: number): Observable<any> {
    const url = `${this.deletePageLinkUrl}?pageId=${pageId}`;
    return this.http.delete(url).pipe(
        catchError(this.handleError)
    );
  }

  GetPageLinks(): Observable<any> {
    return this.http.get(this.getAllPageLinksUrl).pipe(
      catchError(this.handleError)
    );
  }

  GetPageLinkById(id: number): Observable<any> {
    return this.http.get(`${this.getPageLinkByIdUrl}?pageId=${id}`)
      .pipe(catchError(this.handleError)
    );
  }

  GetMenuSubmenu(): Observable<any> {
    return this.http.get(this.getMenuSubmenuUrl).pipe(
      catchError(this.handleError)
    );
  }  

  CreateOrUpdateBanner(formData: FormData, bannerId?: number): Observable<any> {
    const headers = new HttpHeaders();
    const url = bannerId ? `${this.createOrUpdateBannerUrl}?bannerId=${bannerId}` : this.createOrUpdateBannerUrl;
    // Use POST for both creating and updating
    return this.http.post(url, formData, { headers }).pipe(
      catchError(this.handleError)
    );
  }  

  GetBanners(): Observable<any> {
    return this.http.get(this.getAllBannersUrl).pipe(
      catchError(this.handleError)
    );
  }

  GetBannerById(id: number): Observable<any> {
    return this.http.get(`${this.getBannerByIdUrl}?bannerId=${id}`)
      .pipe(catchError(this.handleError)
    );
  }

  GetBannerByName(name: string): Observable<any> {
    return this.http.get(`${this.getBannerByNameUrl}?bannerName=${name}`)
      .pipe(catchError(this.handleError)
    );
  }

  DeleteBanner(id: number): Observable<any> {
    const url = `${this.deleteBannerUrl}?bannerId=${id}`;
    return this.http.delete(url).pipe(
        catchError(this.handleError)
    );
  }

  // Error handling logic
  private handleError(error: any) {
    console.error('An error occurred:', error);
    return throwError(error);
  }
}
