import { Component , QueryList, ViewChildren, ElementRef} from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-complaintregistration',
  templateUrl: './complaintregistration.component.html',
  styleUrls: ['./complaintregistration.component.scss']
})
export class ComplaintregistrationComponent  {

// Otp number

// @ViewChildren('otpInput') otpInputs!: QueryList<ElementRef>;

// otpInputs = new Array(6).fill(''); // Adjust length based on the number of OTP inputs

// onOtpInputChange(event: any, index: number): void {
//   const input = event.target.value;
//   if (input.length === 1 && index < this.otpInputs.length - 1) {
//     this.otpInputs.toArray()[index + 1].nativeElement.focus();
//   }
// }


  files: File[] = [];

	onSelect(event:any) {
		console.log(event);
		this.files.push(...event.addedFiles);
	}

	onRemove(event:any) {
		console.log(event);
		this.files.splice(this.files.indexOf(event), 1);
	}

  currentDate: string = '';
  filesToUpload: File[] = [];
  documentFolderPath = 'http://localhost:44303/assets/ComplaintDocuments/';
  districts: any[] = [];
  blocks: any[] = [];
  gps: any[] = [];
  villages: any[] = [];
  wards: any[]=[];
  categories: any[] = [];
  subcategories: any[] = [];
  complainttype:any[]=[];


  formData: any = {
    ddlRecvBy: [null],
    txtName: '',
    txtPhone: '',
    txtEmail: '',
    txtAddress:'',
    //txtDocument: '',
    ddlDistrict: [null],
    ddlBlock: [null],
    ddlPanchayat: [null],
    ddlVillage: [null],
    ddlward:[null],
    ddlComplaintCategory: [null],
    ddlSubCategory: [null],
    txtDetailsE: '',
    txtLandmark: '',
    ddlComplainttype:[null],
    ddllanguage:'1'
  };

// Upload File
 // Array to hold uploaded files
 uploadedFiles: File[] = [];

 // This method is called when a file is selected
 onFileSelected(event: any): void {
   const files = event.target.files;
   if (files && files.length > 0) {
     for (let i = 0; i < files.length; i++) {
       // Limit the number of files to 4
       if (this.uploadedFiles.length < 4) {
         this.uploadedFiles.push(files[i]);
       } else {
         alert("You can upload only 4 files.");
         break;
       }
     }
   }
 }

 // This method is called when the close (x) icon is clicked for a file
 removeFile(index: number): void {
   // Remove the file from the array
   this.uploadedFiles.splice(index, 1);
 }









  constructor(private http: HttpClient, private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.setCurrentDate();
    this.getDistricts();
    this.getCategories();
    this.getComplaintstype();
  }
  getComplaintstype(){
    //alert("ok");
    this.authService.getcomplainttype().subscribe(
      response=>{
        this.complainttype=response;
        console.log(this.complainttype)
      },
      error=>{
        console.log('Error frtching Complaint status',error)
      }
    )
  }
  getCategories() {
    this.authService.getCategories().subscribe(
      response => {
        this.categories = response;
        console.log(this.categories);
      },
      error => {
        console.error('Error fetching categories', error);
      }
    );
  }

  onCategoryChange(event: any) {

    // Directly use the event as the selected category ID
    const catid = event.inT_CATEGORY_ID;  // event contains the selected category ID directly
    if (catid && !isNaN(catid)) {
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
        console.log(this.districts);
      },
      error => {
        console.error('Error fetching districts', error);
      }
    );
  }
  onUpdateClick() {
    this.router.navigate(['/registrationupdate']);
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

  setCurrentDate() {
    const today = new Date();
    const day = String(today.getDate()).padStart(2, '0');
    const year = today.getFullYear();
    const months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'];
    const month = months[today.getMonth()];
    this.currentDate = `${day}-${month}-${year}`;
  }

  // handleFileInput(event: Event) {
  //
  //   const input = event.target as HTMLInputElement;
  //   if (input.files && input.files.length > 0) {
  //     this.fileToUpload = input.files[0];
  //   } else {
  //     this.fileToUpload = null;
  //   }
  // }
  handleFileInput(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      // Convert FileList to an array and store it
      this.filesToUpload = Array.from(input.files);

    } else {
      this.filesToUpload = [];
    }
  }
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

    this.authService.submitRegistration(formData).subscribe(
      response => {
        alert('Complaint submitted successfully.');
        this.resetForm();
      },
      error => {
        console.error('Error submitting registration data:', error);
        alert('Failed to submit registration data. Please try again.');
      }
    );
  }
//   onSubmit() {
//     if (!this.filesToUpload || this.filesToUpload.length === 0) {
//         alert('Please upload a file before submitting the form.');
//         return;
//     }

//     // Create an object to hold form data
//     const registrationData: any = {
//         NVCH_COMPLIANTANT_NAME: this.formData.txtName || '',
//         VCH_CONTACT_NO: this.formData.txtPhone || '',
//         INT_COMPLIANT_LOG_TYPE: this.formData.ddlRecvBy ? parseInt(this.formData.ddlRecvBy, 10) : 0,
//         INT_DIST_ID: this.formData.ddlDistrict ? parseInt(this.formData.ddlDistrict, 10) : 0,
//         INT_BLOCK: this.formData.ddlBlock ? parseInt(this.formData.ddlBlock, 10) : 0,
//         INT_PANCHAYAT: this.formData.ddlPanchayat ? parseInt(this.formData.ddlPanchayat, 10) : 0,
//         INT_VILLAGE: this.formData.ddlVillage ? parseInt(this.formData.ddlVillage, 10) : 0,
//         INT_WARD: this.formData.ddlWard ? parseInt(this.formData.ddlWard, 10) : 0,
//         INT_CATEGORY_ID: this.formData.ddlComplaintCategory ? parseInt(this.formData.ddlComplaintCategory, 10) : 0,
//         INT_SUB_CATEGORY_ID: this.formData.ddlSubCategory ? parseInt(this.formData.ddlSubCategory, 10) : 0,
//         NVCH_COMPLIANT_DETAILS: this.formData.txtDetailsE || '',
//         NVCH_LANDMARK: this.formData.txtLandmark || '',
//         VCH_EMAIL: this.formData.txtEmail || '',
//         NVCH_ADDRESS: this.formData.txtAddress || ''
//     };

//     // Convert files to base64
//     const filePromises = this.filesToUpload.map(file => this.convertToBase64(file));

//     // After all files are converted to base64, submit the form data
//     Promise.all(filePromises).then(base64Files => {
//         // Add the base64-encoded files to the registrationData object
//         registrationData.files = base64Files;

//         // Log the registration data (including base64 files) for debugging
//         console.log('Registration Data:', registrationData);

//         // Now, send the data to the backend (without FormData)
//         this.authService.submitRegistration(registrationData).subscribe(
//             response => {
//                 alert('Complaint submitted successfully');
//                 this.resetForm();
//                 window.location.reload();
//             },
//             error => {
//                 console.error('Error submitting registration data:', error);
//                 if (error.error.errors) {
//                     alert('Validation errors occurred: ' + error.error.errors.join(', '));
//                 } else {
//                     alert('Failed to submit registration data. Please try again.');
//                 }
//             }
//         );
//     }).catch(error => {
//         console.error('Error converting files to base64:', error);
//         alert('An error occurred while converting files. Please try again.');
//     });
// }

// Helper function to convert a file to base64
// convertToBase64(file: File): Promise<string> {
//     return new Promise((resolve, reject) => {
//         const reader = new FileReader();
//         reader.onloadend = () => {
//             resolve(reader.result as string);
//         };
//         reader.onerror = reject;
//         reader.readAsDataURL(file);
//     });
// }


  // onSubmit() {
  //

  //   // Check if a file is uploaded
  //   if (!this.filesToUpload) {
  //     alert('Please upload a file before submitting the form.');
  //     return;
  //   }

  //   // Call the uploadFile method from your service (returns an Observable)
  //   this.authService.uploadFile(this.filesToUpload).subscribe(
  //     (response) => {
  //       const fileName = response.fileName;
  //       this.submitRegistrationData(fileName);
  //     },
  //     (error) => {
  //       console.error('Error uploading file:', error);
  //       alert('File upload failed. Please try again.');
  //     }
  //   );
  // }


  // uploadFile(): Promise<void> {
  //   return new Promise((resolve, reject) => {
  //     if (this.fileToUpload) {
  //       this.authService.uploadFile(this.fileToUpload).toPromise().then(
  //         (response) => {
  //           console.log('File uploaded successfully', response);
  //           resolve();
  //         },
  //         (error) => {
  //           console.error('Error uploading file', error);
  //           reject(error);
  //         }
  //       );
  //     } else {
  //       reject('No file selected');
  //     }
  //   });
  // }



  // submitRegistrationData(fileName: string) {
  //   const registrationData = {
  //     NVCH_COMPLIANTANT_NAME: this.formData.txtName || '',
  //     VCH_CONTACT_NO: this.formData.txtPhone || '',
  //     //NVCH_ADDRESS:this.formData.txtAddress,
  //     VCH_COMPLAINT_FILE: fileName || this.formData.txtDocument,
  //     INT_COMPLIANT_LOG_TYPE: parseInt(this.formData.ddlRecvBy, 10),
  //     INT_DIST_ID: parseInt(this.formData.ddlDistrict, 10),
  //     INT_BLOCK: parseInt(this.formData.ddlBlock, 10),
  //     INT_PANCHAYAT: parseInt(this.formData.ddlPanchayat, 10),
  //     INT_VILLAGE: parseInt(this.formData.ddlVillage, 10),
  //     INT_WARD:parseInt(this.formData.ddlward,10),
  //     INT_CATEGORY_ID: parseInt(this.formData.ddlComplaintCategory, 10),
  //     INT_SUB_CATEGORY_ID: parseInt(this.formData.ddlSubCategory, 10),
  //     NVCH_COMPLIANT_DETAILS: this.formData.txtDetailsE || '',
  //     NVCH_LANDMARK: this.formData.txtLandmark || '',
  //     VCH_EMAIL: this.formData.txtEmail || '',
  //     //VCH_TOKENNO: this.generateToken(),
  //     NVCH_ADDRESS: this.formData.txtAddress || '' // Ensure this is set correctly
  //   };

  //   // Log the payload for debugging
  //   console.log('Registration Data Payload:', registrationData);

  //   this.authService.submitRegistration(registrationData).subscribe(
  //     (response) => {
  //       alert('Complaint submitted successfully');
  //       //alert(response); // Show the text response
  //       this.resetForm();
  //       window.location.reload();
  //     },
  //     (error) => {
  //       console.error('Error submitting registration data:', error);
  //       alert('Failed to submit registration data. Please try again.');
  //     }
  //   );
  // }
  // generateToken(): string {
  //   // Generate a random 10-digit number
  //   const min = 1000000000; // Smallest 10-digit number
  //   const max = 9999999999; // Largest 10-digit number
  //   const token = Math.floor(Math.random() * (max - min + 1)) + min;
  //   return token.toString(); // Convert to string and return
  // }
  resetForm() {
    this.formData = {
    ddlRecvBy: [null],
    txtName: '',
    txtPhone: '',
    txtEmail: '',
    txtAddress:'',
    //txtDocument: '',
    ddlDistrict: [null],
    ddlBlock: [null],
    ddlPanchayat: [null],
    ddlVillage: [null],
    ddlward:[null],
    ddlComplaintCategory: [null],
    ddlSubCategory: [null],
    txtDetailsE: '',
    txtLandmark: '',
    ddlComplainttype:[null]
    };
    this.filesToUpload = [];
    this.setCurrentDate();
  }
}
