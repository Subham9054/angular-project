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
  fileToUpload: File | null = null;
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
    ddlRecvBy: '0',
    txtName: '',
    txtPhone: '',
    txtEmail: '',
    txtAddress:'',
    txtDocument: '',
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
        //console.log(this.complaintstatus)
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
    debugger;
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
    debugger;
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
    debugger;
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
    debugger;
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
    debugger;
    const villageid = event.inT_VILLAGE_ID;
    if (!isNaN(villageid)) {
      this.authService.wards(villageid).subscribe(
        response => {
          console.log("OK" +response)
          this.wards = response;
         // console.log(this.wards);
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

  handleFileInput(event: Event) {
    debugger;
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.fileToUpload = input.files[0];
    } else {
      this.fileToUpload = null;
    }
  }
  
  onSubmit() {
    debugger;
  
    // Check if a file is uploaded
    if (!this.fileToUpload) {
      alert('Please upload a file before submitting the form.');
      return;
    }
  
    // Call the uploadFile method from your service (returns an Observable)
    this.authService.uploadFile(this.fileToUpload).subscribe(
      (response) => {
        const fileName = response.fileName;
        this.submitRegistrationData(fileName);
      },
      (error) => {
        console.error('Error uploading file:', error);
        alert('File upload failed. Please try again.');
      }
    );
  }
  

  uploadFile(): Promise<void> {
    return new Promise((resolve, reject) => {
      if (this.fileToUpload) {
        this.authService.uploadFile(this.fileToUpload).toPromise().then(
          (response) => {
            console.log('File uploaded successfully', response);
            resolve();
          },
          (error) => {
            console.error('Error uploading file', error);
            reject(error);
          }
        );
      } else {
        reject('No file selected');
      }
    });
  }
  
  

  submitRegistrationData(fileName: string) {
    const registrationData = {
      NVCH_COMPLIANTANT_NAME: this.formData.txtName || '',
      VCH_CONTACT_NO: this.formData.txtPhone || '',
      //NVCH_ADDRESS:this.formData.txtAddress,
      VCH_COMPLAINT_FILE: fileName || this.formData.txtDocument,
      INT_COMPLIANT_LOG_TYPE: parseInt(this.formData.ddlRecvBy, 10),
      INT_DIST_ID: parseInt(this.formData.ddlDistrict, 10),
      INT_BLOCK: parseInt(this.formData.ddlBlock, 10),
      INT_PANCHAYAT: parseInt(this.formData.ddlPanchayat, 10),
      INT_VILLAGE: parseInt(this.formData.ddlVillage, 10),
      INT_WARD:parseInt(this.formData.ddlward,10),
      INT_CATEGORY_ID: parseInt(this.formData.ddlComplaintCategory, 10),
      INT_SUB_CATEGORY_ID: parseInt(this.formData.ddlSubCategory, 10),
      NVCH_COMPLIANT_DETAILS: this.formData.txtDetailsE || '',
      NVCH_LANDMARK: this.formData.txtLandmark || '',
      VCH_EMAIL: this.formData.txtEmail || '',
      //VCH_TOKENNO: this.generateToken(),
      NVCH_ADDRESS: this.formData.txtAddress || '' // Ensure this is set correctly
    };

    // Log the payload for debugging
    console.log('Registration Data Payload:', registrationData);

    this.authService.submitRegistration(registrationData).subscribe(
      (response) => {
        alert('Complaint submitted successfully');
        //alert(response); // Show the text response
        this.resetForm();
        window.location.reload();
      },
      (error) => {
        console.error('Error submitting registration data:', error);
        alert('Failed to submit registration data. Please try again.');
      }
    );
  }
  // generateToken(): string {
  //   // Generate a random 10-digit number
  //   const min = 1000000000; // Smallest 10-digit number
  //   const max = 9999999999; // Largest 10-digit number
  //   const token = Math.floor(Math.random() * (max - min + 1)) + min;
  //   return token.toString(); // Convert to string and return
  // }
  resetForm() {
    this.formData = {
      ddlRecvBy: 0,
    txtName: '',
    txtPhone: '',
    txtEmail: '',
    txtAddress:'',
    txtDocument: '',
    ddlDistrict: '0',
    ddlBlock: '0',
    ddlPanchayat: '0',
    ddlVillage: '0',
    ddlward:'0',
    ddlComplaintCategory: '0',
    ddlSubCategory: '0',
    txtDetailsE: '',
    txtLandmark: '',
    ddlComplainttype:'0'
    };
    this.fileToUpload = null;
    this.setCurrentDate();
  }
}
