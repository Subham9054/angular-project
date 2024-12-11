import { Component } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { ValidatorChecklistService } from 'src/app/services/validator-checklist.service';
import { CommonserviceService } from 'src/app/services/commonservice.service';
import { environment } from 'src/environments/environment';
import { Buffer } from 'buffer';
import * as CryptoJS from 'crypto-js';


@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.scss']
})
export class UserRegistrationComponent {
  userRegistration: FormGroup;
  showpassword: boolean = false;
  toggletype: string = 'password';
  districts: any[] = [];
  blocks: any[] = [];
  gps: any[] = [];
  villages: any[] = [];
  designations: any[] = [];


  constructor(private http: HttpClient,
    private authService: AuthService,
    private router: Router,
    private fb: FormBuilder,
    public vldChkLst: ValidatorChecklistService,
    public commonService: CommonserviceService,


  ) {
    this.userRegistration = this.fb.group({
      userid: '',
      userfullname: '',
      password: '',
      district: [null],
      block: [null],
      Designation: 0,
      txtEmail: '',
      telephone: '',
      Mobileno: '',
      Gender: '',
      ArrayName: '',
      status: '',
      group: 0

    });
  }

  ngOnInit(): void {
    this.getDistricts();
    this.getDesignation();
  }
  submitForm() {
    const formData = this.userRegistration.value;
    if (this.validate_userRegistration_form(formData)) {
      this.commonService.insertData(this.userRegistration.value, 'complaint/ComplaintForm/submitForm').subscribe({
        next: (response: any) => {
          const responseData = response.RESPONSE_DATA;
          const respToken = response.RESPONSE_TOKEN;
          const verifyToken = CryptoJS.HmacSHA256(responseData, environment.apiHashingKey).toString();
          if (respToken == verifyToken) {
            let res: any = Buffer.from(responseData, 'base64');
            res = JSON.parse(res.toString());
            // if (res.status == 200 || res.status == 202) {
            //   let resultValue = res.complainId;
            //   const encryptedResult = this.encDec.encText(resultValue.toString());
            //   const decryptID = this.encDec.decText(encryptedResult);

            // } else {

            //   this.ngxService.stopLoader('loader-al');
            //   this.alertHelper.errorAlert(
            //     res.msg,
            //     "Error"
            //   );

            // }
          } else {
            // this.ngxService.stopLoader('loader-al');
            // this.alertHelper.errorAlert(
            //   commonDataMsg.TOKEN_MISMATCH,
            //   "Error"
            // );
          }
        },
        error: (error: any) => {
          // this.commonService.commonSwalFire('error', error, '', this.new_complainant);
        }
      });// end of subscription

    }

  }
  getDistricts() {
    this.authService.getDistricts().subscribe(
      response => {
        this.districts = response;
      },
      error => {
        console.error('Error fetching districts', error);
      }
    );
  }
  onDistrictChange(event: any) {
    const distId = event.inT_DIST_ID
    if (!isNaN(distId)) {
      this.authService.getBlocks(distId).subscribe(
        response => {
          this.blocks = response;
          this.gps = [];
          this.villages = [];
        },
        error => {
          console.error('Error fetching blocks', error);
        }
      );
    } else {
      console.error('Invalid district ID');
    }
  }
  getDesignation() {
    this.authService.getDesignation().subscribe(
      response => {
        this.designations = response;
      },
      error => {
        console.error('Error fetching designations', error);
      }
    );
  }

  // Method to toggle password visibility
  enableDisableBtn() {
    this.showpassword = !this.showpassword;
    this.toggletype = this.showpassword ? 'text' : 'password';
  }

  showPassword: boolean = false;

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  validate_userRegistration_form(formData: any): boolean {
    let errFlag = 0;
    let userid = formData.userid;
    let userfullname = formData.userfullname;
    let password = formData.password;
    let district = formData.district;
    let block = formData.block;
    let Designation = formData.Designation;
    let telephone = formData.telephone;
    let Mobileno = formData.Mobileno;
    let txtEmail = formData.txtEmail;
    let isGenderValid = formData.Gender;
    let status = formData.status;
    let group = formData.group;




    if (errFlag == 0 && !this.vldChkLst.blankCheck(userid,
      `User ID cannot be blank!`, 'userid')
    ) {
      errFlag = 1;
    }
    if (errFlag == 0 && !this.vldChkLst.isSpecialCharKey('userid', userid,
      `User ID`)
    ) {
      errFlag = 1;
    }
    if (errFlag == 0 && !this.vldChkLst.blankCheck(userfullname,
      `User Full Name cannot be blank!`, 'userfullname')
    ) {
      errFlag = 1;
    }
    if (errFlag == 0 &&
      !this.vldChkLst.isCharecterKey('userfullname', userfullname, 'In Full Name')
    ) {
      errFlag = 1;
    }
    if (errFlag == 0 && !this.vldChkLst.blankCheck(password,
      `Password cannot be blank!`, 'password')
    ) {
      errFlag = 1;
    }

    if (errFlag == 0 &&
      !this.vldChkLst.selectDropdown(district, `District`)
    ) {
      errFlag = 1;
    }
    if (errFlag == 0 &&
      !this.vldChkLst.selectDropdown(block, `Block`)
    ) {
      errFlag = 1;
    }
    if (errFlag == 0 &&
      !this.vldChkLst.selectDropdown(Designation, `Designation`)
    ) {
      errFlag = 1;
    }

    if (errFlag == 0 && !this.vldChkLst.blankCheck(telephone,
      `Telephone No. cannot be blank!`, 'telephone')
    ) {
      errFlag = 1;
    }
    if (errFlag == 0 && !this.vldChkLst.maxLength('telephone', telephone, 10,
      `Telephone No.`)
    ) {
      errFlag = 1;
    }
    if (errFlag == 0 && !this.vldChkLst.minLength(telephone, 10,
      `Telephone No.`, 'telephone')
    ) {
      errFlag = 1;
    }

    if (errFlag == 0 && !this.vldChkLst.blankCheck(Mobileno,
      `Mobile No. cannot be blank!`, 'Mobileno')
    ) {
      errFlag = 1;
    }
    if (errFlag == 0 && !this.vldChkLst.maxLength('Mobileno', Mobileno, 10,
      `Mobile No.`)
    ) {
      errFlag = 1;
    }
    if (errFlag == 0 && !this.vldChkLst.minLength(Mobileno, 10,
      `Mobile No.`, 'Mobileno')
    ) {
      errFlag = 1;
    }
    if (
      errFlag == 0 && !this.vldChkLst.validMob(Mobileno, 'Mobileno', `Mobile Number`
      )
    ) {
      errFlag = 1;
    }
    if (errFlag == 0 && !this.vldChkLst.blankCheck(txtEmail,
      `Email ID cannot be blank!`, 'txtEmail')
    ) {
      errFlag = 1;
    }
    if (errFlag == 0 && !this.vldChkLst.validEmail(txtEmail, 'txtEmail', `Email ID`)
    ) {
      errFlag = 1;
    }
    if (errFlag == 0 &&
      !this.vldChkLst.selectDropdown(status, `Status`)
    ) {
      errFlag = 1;
    }
    if (errFlag == 0 &&
      !this.vldChkLst.selectDropdown(group, `Group`)
    ) {
      errFlag = 1;
    }




    if (errFlag == 0) {
      return true;
    } else {
      return false;
    }
  }
}
