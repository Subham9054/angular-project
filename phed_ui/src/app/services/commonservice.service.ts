import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Buffer } from 'buffer';
import * as CryptoJS from 'crypto-js';
import { FormArray, FormControl } from '@angular/forms';

// import { Constant } from 'src/app/core/constants/constant';

import Swal from 'sweetalert2';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommonserviceService {

  constructor(
    private router: Router,
    private http: HttpClient,
  ) { }
  //============= This Function is used for insert data============
  insertData(formParams: any, fname: any): Observable<any> {
    let requestParam = Buffer.from(JSON.stringify(formParams), 'utf8').toString('base64');
    let requestToken = CryptoJS.HmacSHA256(requestParam, environment.apiHashingKey).toString();
    let reqData = { 'REQUEST_DATA': requestParam, 'REQUEST_TOKEN': requestToken };
    let serviceUrl = environment.serviceURLComplain + fname;
    let serviceRes = this.http.post(serviceUrl, reqData);
    return serviceRes;
  }
}
