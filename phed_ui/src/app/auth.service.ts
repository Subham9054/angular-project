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

  private baseUrl: string = 'https://localhost:7197';
  

  private apiUrl = `${this.baseUrl}/gateway/Login`; // Your API URL for login
  private userregdurl=`${this.baseUrl}/gateway/UserRegistration`;
  private complaintApiUrl = `${this.baseUrl}/gateway/ComplaintCategory`;
  private getComplaintApiUrl = `${this.baseUrl}/gateway/GetallComplaint`;
  private updateComplaintApiUrl = `${this.baseUrl}/gateway/UpdateComplaint`;
  //private getComplaintByIdApiUrl = 'http://172.27.32.0:8085/api/Complaint/GetComplaintById';
  private deleteapiurl = `${this.baseUrl}/gateway/deleteComplaintbyid`;
  private districturl =  `${this.baseUrl}/gateway/GetDistricts`;  
  private blockurl =  `${this.baseUrl}/gateway/GetBlocks`;
  private gpurl =  `${this.baseUrl}/gateway/GetGp`;
  private villageurl =  `${this.baseUrl}/gateway/GetVillages`;
  private wardurl= `${this.baseUrl}/gateway/GetWards`
  private categoryUrl =  `${this.baseUrl}/gateway/GetCategory`;
  private subcategoryUrl =  `${this.baseUrl}/gateway/GetSubCategories`;
  private fileUploadUrl =  `${this.baseUrl}/gateway/UploadFile`;
  private complaintstatusUrl = `${this.baseUrl}/gateway/GetComplaints`;
  private complainttypeurl =  `${this.baseUrl}/gateway/GetComplaintstype`;
  private getdesignationurl= `${this.baseUrl}/gateway/GetDesignation`;
  private getloclevel= `${this.baseUrl}/gateway/GetLocationLevel`;
  private insertescalationurl=`${this.baseUrl}/gateway/insertescalation`;
  private checkApiUrl = `${this.baseUrl}/gateway/check`;
  private viewEscalationurl=`${this.baseUrl}/gateway/viewescalation`;
  private viewEscalationurleye= `${this.baseUrl}/gateway/viewescalationeye`;
  private viewupdatepenurl=`${this.baseUrl}/gateway/viewupdatepen`;
  private getPrioritiesUrl=`${this.baseUrl}/gateway/GetComplaintPriority`;
  private submitsubcaturl=`${this.baseUrl}/gateway/ComplaintSubCategory`;
  private getallsubcaturl = `${this.baseUrl}/gateway/ViewComplaintSubCategory`;
  private updatesubcaturl=`${this.baseUrl}/gateway/UpdateComplaintSubCategory`;
  private deletesubcaturl= `${this.baseUrl}/gateway/DeleteSubcat`;
  //GMS
  private registrationApiUrl = `${this.baseUrl}/gateway/DetailcomplaintRegistration`;
  private gmsComplaintdetailurl=`${this.baseUrl}/gateway/GetGmsComplaintdetails`;
  private gmstakeactionurl=`${this.baseUrl}/gateway/Getgmstakeaction`;
  private GetAllDetailsagainsttokenurl=`${this.baseUrl}/gateway/GetAllDetailsagainsttoken`;
  private GetCitizenAddressDetailsurl=`${this.baseUrl}/gateway/GetCitizenAddressDetails`;
  private UpdateCitizenAddressDetailsurl=`${this.baseUrl}/gateway/UpdateCitizenAddressDetails`;
  private GetAllCitizenDetailsurl=`${this.baseUrl}/gateway/GetAllCitizenDetails`;
  private GetAllComplaintsurl=`${this.baseUrl}/gateway/GetAllComplaints`;
  private Otpgenerateurl=`${this.baseUrl}/gateway/Otpgenerate`;
  private ValidateOtpurl=`${this.baseUrl}/gateway/ValidateOtp`;



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

  private cmsBaseURL = 'http://localhost:5097/api/CMS'; //Base URL for Managing CMS Master Pages
  private galleryUrl = 'http://localhost:5097/api/Gallery'; //Base URL for Managing Gallery
  private faqUrl = 'http://localhost:5097/api/FAQ'; //Base URL for Managing FAQs

  constructor(private http: HttpClient) { }
  
 
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

  wards( villageid: number): Observable<any> {
    return this.http.get<any>(`${this.wardurl}?villageid=${villageid}`).pipe(
      catchError(this.handleError)
    );
  }
  // Registration data submission
  // submitRegistration(registrationData: any): Observable<any> {
  //   return this.http.post(this.registrationApiUrl, registrationData, {
  //     headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  //     responseType: 'text' // Set response type to text
  //   }).pipe(
  //     catchError(this.handleError)
  //   );
  // }
  submitRegistration(formData: FormData): Observable<any> {
    alert('1');
    console.log('FormData Contents:');
    formData.forEach((value, key) => {
      console.log(`${key}:`, value);
    });
    return this.http.post(this.registrationApiUrl, formData);
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

  updateComplaint(
    id: number,
    complaintData: { VCH_CATEGORY: string; NVCH_CATEGORY: string }
  ): Observable<any> {
    // Ensure `updateComplaintApiUrl` is correctly initialized in your service
    const url = `${this.updateComplaintApiUrl}/${id}`;
    return this.http.post(url, complaintData).pipe(
      catchError((error) => {
        console.error('Error in updateComplaint:', error);
        return throwError(() => new Error('Error occurred while updating the complaint.'));
      })
    );
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

  // getComplaintById(id: number): Observable<any> {
  //   return this.http.get(`${this.getComplaintByIdApiUrl}/${id}`)
  //     .pipe(catchError(this.handleError));
  // }
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

  updateSubCat(subcatid: number, registrationData: any) {
    return this.http.post<any>(`${this.updatesubcaturl}?subcatid=${subcatid}`, registrationData).pipe(
      catchError(this.handleError)
    );
  }
  
  deleteComplaintSubCategory(catid: string, subcatid: string): Observable<any> {
    return this.http.delete<any>(`${this.deletesubcaturl}?catid=${catid}&subcatid=${subcatid}`).pipe(
      catchError(this.handleError)
    );
  }

  getgmsComplaintdelail(): Observable<any> {
    return this.http.get<any>(this.gmsComplaintdetailurl).pipe(
      catchError(this.handleError)
    );
  }

  getgmstakeaction(token: string): Observable<any> {
    return this.http.get<any>(`${this.gmstakeactionurl}?token=${token}`).pipe(
      catchError(this.handleError)
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

  //Methods for Manage Banner by Debasis Das
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

  //Methods for Manage What's New by Debasis Das
  createOrUpdateWhatIsNew(formData: FormData, id?: number): Observable<any> {
    const headers = new HttpHeaders();
    const url = id ? `${this.cmsBaseURL}/CreateOrUpdateWhatIsNew?whatIsNewId=${id}` : `${this.cmsBaseURL}/CreateOrUpdateWhatIsNew`;
    
    // Use POST for both creating and updating
    return this.http.post(url, formData, { headers }).pipe(
        catchError(this.handleError)
    );
  }

  getWhatIsNews(): Observable<any> {
    return this.http.get(`${this.cmsBaseURL}/GetWhatIsNews`).pipe(
      catchError(this.handleError)
    );
  }

  getWhatIsNewById(id: number): Observable<any> {
    return this.http.get(`${this.cmsBaseURL}/GetWhatIsNewById?whatIsNewId=${id}`).pipe(
      catchError(this.handleError)
    );
  }

  getWhatIsNewByName(name: string): Observable<any> {
    return this.http.get(`${this.cmsBaseURL}/GetWhatIsNewName?whatIsNewName=${name}`).pipe(
      catchError(this.handleError)
    );
  }

  deleteWhatIsNew(id: number): Observable<any> {
    return this.http.delete(`${this.cmsBaseURL}/DeleteWhatIsNew?whatIsNewId=${id}`).pipe(
      catchError(this.handleError)
    );
  }  

  //Methods for Manage News & Events by Debasis Das
  createOrUpdateEvent(formData: FormData, id?: number): Observable<any> {
    const headers = new HttpHeaders();
    const url = id ? `${this.cmsBaseURL}/CreateOrUpdateEvent?eventId=${id}` : `${this.cmsBaseURL}/CreateOrUpdateEvent`;
    
    // Use POST for both creating and updating
    return this.http.post(url, formData, { headers }).pipe(
        catchError(this.handleError)
    );
  }

  getEvents(): Observable<any> {
    return this.http.get(`${this.cmsBaseURL}/GetEvents`).pipe(
      catchError(this.handleError)
    );
  }

  getEventById(id: number): Observable<any> {
    return this.http.get(`${this.cmsBaseURL}/GetEventById?eventId=${id}`).pipe(
      catchError(this.handleError)
    );
  }

  getEventByName(name: string): Observable<any> {
    return this.http.get(`${this.cmsBaseURL}/GetEventByName?eventName=${name}`).pipe(
      catchError(this.handleError)
    );
  }

  deleteEvent(id: number): Observable<any> {
    return this.http.delete(`${this.cmsBaseURL}/DeleteEvent?eventId=${id}`).pipe(
      catchError(this.handleError)
    );
  }

  //Methods for Manage Gallery by Debasis Das
  createOrUpdateGallery(formData: FormData, id?: number): Observable<any> {
    const headers = new HttpHeaders();
    const url = id ? `${this.galleryUrl}/CreateOrUpdateGallery?galleryId=${id}` : `${this.galleryUrl}/CreateOrUpdateGallery`;
    
    // Use POST for both creating and updating
    return this.http.post(url, formData, { headers }).pipe(
        catchError(this.handleError)
    );
  }

  getGallery(): Observable<any> {
    return this.http.get(`${this.galleryUrl}/GetGallery`).pipe(
      catchError(this.handleError)
    );
  }

  getGalleryById(id: number): Observable<any> {
    return this.http.get(`${this.galleryUrl}/GetGalleryById?galleryId=${id}`).pipe(
      catchError(this.handleError)
    );
  }

  getGalleryByName(name: string): Observable<any> {
    return this.http.get(`${this.galleryUrl}/GetGalleryByName?galleryName=${name}`).pipe(
      catchError(this.handleError)
    );
  } 

  deleteGallery(id: number): Observable<any> {
    return this.http.delete(`${this.galleryUrl}/DeleteGallery?galleryId=${id}`).pipe(
      catchError(this.handleError)
    );
  }  

  //Methods for Manage FAQs by Debasis Das
  createOrUpdateFAQ(faqData: any): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });  
    return this.http.post(`${this.faqUrl}/CreateOrUpdateFaq`, faqData, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  getFAQs(): Observable<any> {
    return this.http.get(`${this.faqUrl}/GetFaqs`).pipe(
      catchError(this.handleError)
    );
  }

  getFAQById(faqId: number): Observable<any> {
    return this.http.get(`${this.faqUrl}/GetFaqById?faqId=${faqId}`).pipe(
      catchError(this.handleError)
    );
  }

  // getFAQById(id: number): Observable<FAQ> {
  //   return this.http.get<FAQ>(`${this.baseUrl}/faq/${id}`);
  // }

  deleteFAQ(id: number): Observable<any> {
    return this.http.delete(`${this.faqUrl}/DeleteFaq`, { body: { FaqId: id } }).pipe(
      catchError(this.handleError)
    );
  }

  // deleteFAQ(id: number): Observable<any> {
  //   return this.http.delete(`${this.faqUrl}/DeleteFaqDetails?faqId=${id}`).pipe(
  //     catchError(this.handleError)
  //   );
  // }

  //Methods for Manage Contacts by Debasis Das
  createOrUpdateContact(formData: FormData, id?: number): Observable<any> {
    const headers = new HttpHeaders();
    const url = id ? `${this.cmsBaseURL}/CreateOrUpdateContact?contactId=${id}` : `${this.cmsBaseURL}/CreateOrUpdateContact`;
    
    // Use POST for both creating and updating
    return this.http.post(url, formData, { headers }).pipe(
        catchError(this.handleError)
    );
  }

  getContacts(): Observable<any> {
    return this.http.get(`${this.cmsBaseURL}/GetContacts`).pipe(
      catchError(this.handleError)
    );
  }

  getContactById(id: number): Observable<any> {
    return this.http.get(`${this.cmsBaseURL}/GetContactById?contactId=${id}`).pipe(
      catchError(this.handleError)
    );
  }

  deleteContact(id: number): Observable<any> {
    return this.http.delete(`${this.cmsBaseURL}/DeleteContact?contactId=${id}`).pipe(
      catchError(this.handleError)
    );
  }

  // Error handling logic
  private handleError(error: any) {
    console.error('An error occurred:', error);
    return throwError(() => new Error(error.message || 'Unknown error occurred'));
  }

  // private handleError(error: any): Observable<never> {
  //   console.error('An error occurred:', error); // For debugging
  //   return throwError(error.message || 'Server Error');
  // }
}
