import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import Swal from 'sweetalert2';
import { ChangeDetectorRef } from '@angular/core';
declare var $:any;


@Component({
  selector: 'app-registrationcomplaint',
  templateUrl: './registrationcomplaint.component.html',
  styleUrls: ['./registrationcomplaint.component.scss']
})
export class RegistrationcomplaintComponent implements OnInit {

  fileName: string = '';
  otpValues: string[] = ['', '', '', '', '', '']; // OTP input values
  isModalVisible = true; // Initially set the modal to visible
  files: File[] = [];
  currentDate: string = '';
  fileToUpload: File | null = null;
  documentFolderPath = 'http://localhost:44303/assets/ComplaintDocuments/';
  districts: any[] = [];
  blocks: any[] = [];
  gps: any[] = [];
  villages: any[] = [];
  categories: any[] = [];
  subcategories: any[] = [];
  complainttype: any[] = [];

  formData: any = {
    ddlRecvBy: 0,
    txtName: '',
    txtPhone: '',
    txtEmail: '',
    txtAddress: '',
    txtDocument: '',
    ddlDistrict: '0',
    ddlBlock: '0',
    ddlPanchayat: '0',
    ddlVillage: '0',
    ddlComplaintCategory: '0',
    ddlSubCategory: '0',
    txtDetailsE: '',
    txtLandmark: '',
    ddlComplainttype: '0',
    ddllanguage: '1'
  };

  constructor(
    private http: HttpClient,
    private authService: AuthService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.getCategories();
    this.getDistricts();
  }

  doSomething() {
    $('#verifyMobileModal').modal('hide')
  }

  onSelect(event: any) {
    this.files.push(...event.addedFiles);
  }

  onRemove(event: any) {
    this.files.splice(this.files.indexOf(event), 1);
  }

  handleFileInput(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.fileName = file.name;
    }
  }
  // handleFileInput(event: Event) {
  //   const input = event.target as HTMLInputElement;
  //   if (input.files && input.files.length > 0) {
  //     this.fileToUpload = input.files[0];
  //   } else {
  //     this.fileToUpload = null;
  //   }
  // }

  getCategories() {
    this.authService.getCategories().subscribe(
      response => {
        this.categories = response;
      },
      error => {
        console.error('Error fetching categories', error);
      }
    );
  }

  onCategoryChange(event: any) {
    const catid = parseInt(event.target.value, 10);
    if (!isNaN(catid)) {
      this.authService.getSubcategories(catid).subscribe(
        response => {
          this.subcategories = response;
        },
        error => {
          console.error('Error fetching subcategories', error);
        }
      );
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
    const distId = parseInt(event.target.value, 10);
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
    }
  }

  onBlockChange(event: any) {
    const blockId = parseInt(event.target.value, 10);
    const distId = parseInt(this.formData.ddlDistrict, 10);
    if (!isNaN(distId) && !isNaN(blockId)) {
      this.authService.getGps(distId, blockId).subscribe(
        response => {
          this.gps = response;
        },
        error => {
          console.error('Error fetching GPs', error);
        }
      );
    }
  }

  onGpChange(event: any) {
    const gpId = parseInt(event.target.value, 10);
    const blockId = parseInt(this.formData.ddlBlock, 10);
    const distId = parseInt(this.formData.ddlDistrict, 10);
    if (!isNaN(distId) && !isNaN(blockId) && !isNaN(gpId)) {
      this.authService.getVillages(distId, blockId, gpId).subscribe(
        response => {
          this.villages = response;
        },
        error => {
          console.error('Error fetching villages', error);
        }
      );
    }
  }

  onSubmit() {
    if (!this.fileToUpload) {
      alert('Please upload a file before submitting the form.');
      return;
    }

    this.uploadFile().then((fileName) => {
      this.submitRegistrationData(fileName);
    }).catch(error => {
      console.error('Error uploading file:', error);
      Swal.fire('Error', 'File upload failed. Please try again.', 'error');
    });
  }

  mobOtp() {
    const otpInput = this.otpValues.join('');
    this.checkPassword(otpInput);
  }

  checkPassword(otpInput: string) {
    const correctPassword = '123456'; // predefined OTP
    if (otpInput === correctPassword) {
      Swal.fire({
        title: 'Success!',
        text: 'OTP verified successfully!',
        icon: 'success',
        confirmButtonText: 'OK'
      }).then((result) => {
        if (result.isConfirmed) {
          this.closeModal();
          this.resetOtpFields();
        }
      });
    } else {
      Swal.fire({
        title: 'Error!',
        text: 'Incorrect OTP!',
        icon: 'error',
        confirmButtonText: 'Try Again'
      }).then(() => {
        setTimeout(() => {
          this.resetOtpFields();
        }, 2000);
      });
    }
  }

  resetOtpFields() {
    this.otpValues = ['', '', '', '', '', ''];
  }

  openModal() {
    $('#verifyMobileModal').modal('show')
  }

  closeModal() {
    $('#verifyMobileModal').modal('hide')
  }

  uploadFile(): Promise<string> {
    return new Promise((resolve, reject) => {
      const fileUploadData = new FormData();
      fileUploadData.append('file', this.fileToUpload!, this.fileToUpload!.name);
  
      this.http.post<{ message: string; fileName: string }>('https://localhost:7225/api/Dropdown/UploadFile', fileUploadData)
        .subscribe(
          (response) => {
            resolve(response.fileName);
          },
          error => {
            Swal.fire('Error', 'File upload failed: ' + error.message, 'error');
            reject(error);
          }
        );
    });
  }
  

  submitRegistrationData(fileName: string) {
    const registrationData = {
      NVCH_COMPLIANTANT_NAME: this.formData.txtName || '',
      VCH_CONTACT_NO: this.formData.txtPhone || '',
	  VCH_COMPLAINT_FILE: fileName || this.formData.txtDocument,
      INT_COMPLIANT_LOG_TYPE: parseInt(this.formData.ddlRecvBy, 10),
      INT_DIST_ID: parseInt(this.formData.ddlDistrict, 10),
      INT_BLOCK: parseInt(this.formData.ddlBlock, 10),
      INT_PANCHAYAT: parseInt(this.formData.ddlPanchayat, 10),
      INT_VILLAGE: parseInt(this.formData.ddlVillage, 10),
      INT_CATEGORY_ID: parseInt(this.formData.ddlComplaintCategory, 10),
      INT_SUB_CATEGORY_ID: parseInt(this.formData.ddlSubCategory, 10),
      NVCH_COMPLIANT_DETAILS: this.formData.txtDetailsE || '',
      NVCH_LANDMARK: this.formData.txtLandmark || '',
      VCH_EMAIL: this.formData.txtEmail || '',
      VCH_TOKENNO: this.generateToken(),
      NVCH_ADDRESS: this.formData.txtAddress || ''
    };

    this.authService.submitRegistration(registrationData).subscribe(
      (response) => {
        console.log(response);
        Swal.fire({
          title: 'Success!',
          text: 'Complaint submitted successfully',
          icon: 'success',
          confirmButtonText: 'OK'
        }).then((result) => {
          if (result.isConfirmed) {
            this.resetForm();
            // Optional: navigate to a specific page or refresh specific parts instead of reloading
          }
        });
      },
      (error) => {
        Swal.fire({
          title: 'Error',
          text: 'Failed to submit registration data. Please try again.',
          icon: 'error',
          confirmButtonText: 'OK'
        });
      }
    );
    
  }

  generateToken(): string {
    const min = 1000000000;
    const max = 9999999999;
    return (Math.floor(Math.random() * (max - min + 1)) + min).toString();
  }

  resetForm() {
    this.formData = {
      ddlRecvBy: 0,
      txtName: '',
      txtPhone: '',
      txtEmail: '',
      txtAddress: '',
      txtDocument: '',
      ddlDistrict: '0',
      ddlBlock: '0',
      ddlPanchayat: '0',
      ddlVillage: '0',
      ddlComplaintCategory: '0',
      ddlSubCategory: '0',
      txtDetailsE: '',
      txtLandmark: '',
      ddlComplainttype: '0',
      ddllanguage: '1'
    };
    this.files = [];
    this.otpValues = ['', '', '', '', '', ''];
  }
}
