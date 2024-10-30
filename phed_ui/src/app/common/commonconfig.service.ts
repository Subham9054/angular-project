import { Injectable } from '@angular/core';
import { environment } from "src/environments/environment";
import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import * as CryptoJS from "crypto-js";
import { Subscription} from 'rxjs/internal/Subscription';
import Swal from 'sweetalert2';
@Injectable({
  providedIn: 'root'
})
export class CommonconfigService {

  serviceURL = environment.apiUrl;
  constructor(private http:HttpClient) { }
  public getLanguage(formData: any): Observable<any> {
    let requestParam = btoa(encodeURIComponent(JSON.stringify(formData)));
    let requestToken = CryptoJS.HmacSHA256(requestParam, environment.apiHashingKey).toString();
    formData = { 'REQUEST_DATA': requestParam, 'REQUEST_TOKEN': requestToken }
    let serviceURL = environment.apiUrl + "Login";
    let moduleResponse = this.http.get(serviceURL);
    return moduleResponse;
  }
}
