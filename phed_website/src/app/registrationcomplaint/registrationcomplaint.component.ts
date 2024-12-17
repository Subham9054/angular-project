import { Component, OnInit, ViewChildren, QueryList, ElementRef } from '@angular/core';
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

  timer: number = 120; // Timer duration in seconds
  interval: any; // For holding interval reference

  @ViewChildren('otp0, otp1, otp2, otp3, otp4, otp5') otpInputs!: QueryList<ElementRef>;

  fileName: string = '';
  otpValues: string[] = ['', '', '', '', '', ''];
  //isModalVisible = true;
  files: File[] = [];
  currentDate: string = '';
  fileToUpload: File | null = null;
  documentFolderPath = 'http://localhost:44303/assets/ComplaintDocuments/';
  districts: any[] = [];
  blocks: any[] = [];
  gps: any[] = [];
  villages: any[] = [];
  wards:any[]=[];
  categories: any[] = [];
  subcategories: any[] = [];
  complainttype: any[] = [];
  gpnid: number=0;
  wrdid: number=0;
  filesToUpload: File[] = [];
  
  formData: any = {
    ddlRecvBy: 0,
    txtName: '',
    txtPhone: '',
    txtEmail: '',
    txtAddress: '',
    txtDocument: '',
    ddlDistrict: [],
    ddlBlock: [],
    ddlPanchayat: [],
    ddlVillage: [],
    ddlWard: [],
    ddlComplaintCategory: [],
    ddlSubCategory: '0',
    txtDetailsE: '',
    txtLandmark: '',
    ddlComplainttype: '0',
    ddllanguage: '1'
  };

 // Select upload
items = [
  { id: 1, name: 'Option 1' },
  { id: 2, name: 'Option 2' },
  { id: 3, name: 'Option 3' },
  // Add more options as needed
];

selectedItem: any;

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

  handleFileInput(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      // Convert FileList to an array and store it
      this.filesToUpload = Array.from(input.files);
      
    } else {
      this.filesToUpload = [];
    }
  }
  

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
    debugger;
    const catid = event.inT_CATEGORY_ID;
    //const catid = event;  // event itself contains the value of the selected category
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
  

 
 

  startTimer() {
    this.stopTimer();
    this.timer = 120;
    this.interval = setInterval(() => {
      if (this.timer > 0) {
        this.timer--;
        console.log(this.timer); // Debug: shows the countdown in the console
      } else {
        this.stopTimer();
      }
    }, 1000);
  }

  stopTimer() {
    
      clearInterval(this.interval);
    
  }

  get formattedTime(): string {
    const minutes = Math.floor(this.timer / 60);
    const seconds = this.timer % 60;
    return `${this.padZero(minutes)}:${this.padZero(seconds)}`;
}

// Helper function to add a leading zero if needed
padZero(value: number): string {
    return value < 10 ? `0${value}` : value.toString();
}

  moveToNext(index: number) {
    if (this.otpValues[index] && index < 5) {
      const nextInput = this.otpInputs.toArray()[index + 1];
      if (nextInput) {
        nextInput.nativeElement.focus();
      }
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
    debugger;
    const distId = event.inT_DIST_ID;  // Get the district ID from the selected option
  
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
  
  onBlockChange(event: any) {

    const blockId = event.inT_BLOCK_ID
    if (!isNaN(blockId)) {
      this.authService.getGps(blockId).subscribe(
        response => {
          this.gps = response;
          console.log(this.gps);
        },
        error => {
          console.error('Error fetching GPs', error);
        }
      );
    } else {
      console.error('Invalid district or block ID');
    }
  }

  onGpChange(event: any) {

    const gpId = event.inT_GP_ID;
    if (!isNaN(gpId)) {
      this.authService.getVillages(gpId).subscribe(
        response => {
          this.villages = response;
          console.log(this.villages);
        },
        error => {
          console.error('Error fetching villages', error);
        }
      );
    } else {
      console.error('Invalid district, block, or GP ID');
    }
  }
  onVillage(event: any) {

    const villageid = event.inT_VILLAGE_ID;
    if (!isNaN(villageid)) {
      this.authService.wards(villageid).subscribe(
        response => {
          console.log("OK" +response)
          this.wards = response;
          //console.log(this.wards);
        },
        error => {
          console.error('Error fetching villages', error);
        }
      );
    } else {
      console.error('Invalid district, block, or GP ID');
    }
  }
  // onSubmit() {
  //   if (!this.fileToUpload) {
  //     alert('Please upload a file before submitting the form.');
  //     return;
  //   }

  //   this.uploadFile().then((fileName) => {
  //     this.submitRegistrationData(fileName);
  //   }).catch(error => {
  //     console.error('Error uploading file:', error);
  //     Swal.fire('Error', 'File upload failed. Please try again.', 'error');
  //   });
  // }

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
    this.startTimer();
    $('#verifyMobileModal').modal('show')
    this.startTimer();
  }

  closeModal() {
    $('#verifyMobileModal').modal('hide');
    this.resetOtpFields();
    this.stopTimer();
  }

  // uploadFile(): Promise<string> {
  //   return new Promise((resolve, reject) => {
  //     const fileUploadData = new FormData();
  //     fileUploadData.append('file', this.fileToUpload!, this.fileToUpload!.name);
  
  //     this.http.post<{ message: string; fileName: string }>('https://localhost:7225/api/Dropdown/UploadFile', fileUploadData)
  //       .subscribe(
  //         (response) => {
  //           resolve(response.fileName);
  //         },
  //         error => {
  //           Swal.fire('Error', 'File upload failed: ' + error.message, 'error');
  //           reject(error);
  //         }
  //       );
  //   });
  // }
  

  // submitRegistrationData(fileName: string) {
  //   const wardid= $('#ddlward').attr('ng-reflect-model');
  //   const wi=wardid.value;
  //   alert(wi);
  //   debugger;
  //   const registrationData = {
  //     NVCH_COMPLIANTANT_NAME: this.formData.txtName || '',
  //     VCH_CONTACT_NO: this.formData.txtPhone || '',
	//     VCH_COMPLAINT_FILE: fileName || this.formData.txtDocument,
  //     INT_COMPLIANT_LOG_TYPE: parseInt(this.formData.ddlRecvBy, 10),
  //     INT_DIST_ID: parseInt(this.formData.ddlDistrict, 10),
  //     INT_BLOCK: parseInt(this.formData.ddlBlock, 10),
  //     INT_PANCHAYAT: this.gpnid, //parseInt(this.formData.ddlPanchayat, 10),
  //     INT_VILLAGE: parseInt(this.formData.ddlVillage, 10),
  //     //INT_WARD : document.getElementById('ddlward') as HTMLInputElement,
  //     INT_WARD: parseInt($('#ddlward').attr('ng-reflect-model'),10),
  //     INT_CATEGORY_ID: parseInt(this.formData.ddlComplaintCategory, 10),
  //     INT_SUB_CATEGORY_ID: parseInt(this.formData.ddlSubCategory, 10),
  //     NVCH_COMPLIANT_DETAILS: this.formData.txtDetailsE || '',
  //     NVCH_LANDMARK: this.formData.txtLandmark || '',
  //     VCH_EMAIL: this.formData.txtEmail || '',
  //     VCH_TOKENNO: this.generateToken(),
  //     NVCH_ADDRESS: this.formData.txtAddress || ''
  //   };

  //   this.authService.submitRegistration(registrationData).subscribe(
  //     (response) => {
  //       console.log(response);
  //       Swal.fire({
  //         title: 'Success!',
  //         text: 'Complaint submitted successfully',
  //         icon: 'success',
  //         confirmButtonText: 'OK'
  //       }).then((result) => {
  //         if (result.isConfirmed) {
  //           this.resetForm();
  //           // Optional: navigate to a specific page or refresh specific parts instead of reloading
  //         }
  //       });
  //     },
  //     (error) => {
  //       Swal.fire({
  //         title: 'Error',
  //         text: 'Failed to submit registration data. Please try again.',
  //         icon: 'error',
  //         confirmButtonText: 'OK'
  //       });
  //     }
  //   );
    
  // }

  // generateToken(): string {
  //   const min = 1000000000;
  //   const max = 9999999999;
  //   return (Math.floor(Math.random() * (max - min + 1)) + min).toString();
  // }
  onSubmit() {
    if (!this.filesToUpload || this.filesToUpload.length === 0) {
      alert('Please upload a file before submitting the form.');
      return;
    }

    const formData = new FormData();
    formData.append('NVCH_COMPLIANTANT_NAME', this.formData.txtName || '');
    formData.append('VCH_CONTACT_NO', this.formData.txtPhone || '');
    formData.append('INT_COMPLIANT_LOG_TYPE', this.formData.ddlComplainttype ? this.formData.ddlComplainttype.toString() : '0');
    formData.append('INT_DIST_ID', this.formData.ddlDistrict ? this.formData.ddlDistrict.toString() : '0');
    formData.append('INT_BLOCK', this.formData.ddlBlock ? this.formData.ddlBlock.toString() : '0');
    formData.append('INT_PANCHAYAT', this.formData.ddlPanchayat ? this.formData.ddlPanchayat.toString() : '0');
    formData.append('INT_VILLAGE', this.formData.ddlVillage ? this.formData.ddlVillage.toString() : '0');
    formData.append('INT_WARD', this.formData.ddlward ? this.formData.ddlward.toString() : '0');
    formData.append('INT_CATEGORY_ID', this.formData.ddlComplaintCategory ? this.formData.ddlComplaintCategory.toString() : '0');
    formData.append('INT_SUB_CATEGORY_ID', this.formData.ddlSubCategory ? this.formData.ddlSubCategory.toString() : '0');
    formData.append('NVCH_COMPLIANT_DETAILS', this.formData.txtDetailsE || '');
    formData.append('NVCH_LANDMARK', this.formData.txtLandmark || '');
    formData.append('VCH_EMAIL', this.formData.txtEmail || '');
    formData.append('NVCH_ADDRESS', this.formData.txtAddress || '');

    this.filesToUpload.forEach(file => {
      formData.append('files', file, file.name);
    });
    debugger;
    formData.forEach((value, key) => {
      console.log(`${key}:`, value);
    });
    this.authService.submitRegistration(formData).subscribe(
      response => {
        Swal.fire({
          title: 'Success!',
          text: 'Complaint submitted successfully.',
          icon: 'success',
          confirmButtonText: 'OK'
        });
        this.resetForm();
      },
      error => {
        console.error('Error submitting registration data:', error);
        alert('Failed to submit registration data. Please try again.');
      }
    );
  }

  resetForm() {
    this.formData = {
      ddlRecvBy: 0,
      txtName: '',
      txtPhone: '',
      txtEmail: '',
      txtAddress: '',
      txtDocument: '',
      ddlDistrict: [],
      ddlBlock: [],
      ddlPanchayat: [],
      ddlVillage: [],
      ddlComplaintCategory: [],
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
