import { Component } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-complaintregistration',
  templateUrl: './complaintregistration.component.html',
  styleUrls: ['./complaintregistration.component.scss']
})
export class ComplaintregistrationComponent  {
  currentDate: string = '';
  fileToUpload: File | null = null;
  documentFolderPath = 'http://localhost:44303/assets/ComplaintDocuments/';
  districts: any[] = [];
  blocks: any[] = [];
  gps: any[] = [];
  villages: any[] = [];
  categories: any[] = [];
  subcategories: any[] = [];
  complainttype:any[]=[];


  formData: any = {
    ddlRecvBy: 0,
    txtName: '',
    txtPhone: '',
    txtAddress:'',
    txtDocument: '',
    ddlDistrict: '0',
    ddlBlock: '0',
    ddlPanchayat: '0',
    ddlVillage: '0',
    ddlComplaintCategory: '0',
    ddlSubCategory: '0',
    txtDetailsE: '',
    ddlComplainttype:'0',
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
    const catid = parseInt(event.target.value, 10);
    if (!isNaN(catid)) {
      this.authService.getsubcategories(catid).subscribe(
        response => {
          this.subcategories = response;
          console.log(this.subcategories);
        },
        error => {
          console.error('Error fetching subcategories', error);
        }
      );
    } else {
      console.error('Invalid category ID');
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
    } else {
      console.error('Invalid district ID');
    }
  }

  onBlockChange(event: any) {
    const blockId = parseInt(event.target.value, 10);
    const distId = parseInt(this.formData.ddlDistrict, 10);
    if (!isNaN(distId) && !isNaN(blockId)) {
      this.authService.getGps(distId, blockId).subscribe(
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
    const gpId = parseInt(event.target.value, 10);
    const blockId = parseInt(this.formData.ddlBlock, 10);
    const distId = parseInt(this.formData.ddlDistrict, 10);
    if (!isNaN(distId) && !isNaN(blockId) && !isNaN(gpId)) {
      this.authService.getVillages(distId, blockId, gpId).subscribe(
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

  setCurrentDate() {
    const today = new Date();
    const day = String(today.getDate()).padStart(2, '0');
    const year = today.getFullYear();
    const months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'];
    const month = months[today.getMonth()];
    this.currentDate = `${day}-${month}-${year}`;
  }

  handleFileInput(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.fileToUpload = input.files[0];
    } else {
      this.fileToUpload = null;
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
      alert('File upload failed. Please try again.');
    });
  }

  uploadFile(): Promise<string> {
    return new Promise((resolve, reject) => {
      const fileUploadData = new FormData();
      fileUploadData.append('file', this.fileToUpload!, this.fileToUpload!.name);
      
      this.http.post<{ message: string; fileName: string }>('https://localhost:44303/api/ComplaintsRegistration/UploadFile', fileUploadData)
        .subscribe(
          (response) => {
            console.log('File uploaded successfully', response);
            resolve(response.fileName); // Resolving the file name from the response
          },
          error => {
            console.error('Error uploading file', error);
            reject(error);
          }
        );
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
      INT_CATEGORY_ID: parseInt(this.formData.ddlComplaintCategory, 10),
      INT_SUB_CATEGORY_ID: parseInt(this.formData.ddlSubCategory, 10),
      NVCH_COMPLIANT_DETAILS: this.formData.txtDetailsE || '',
      VCH_TOKENNO: this.generateToken(),
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
  generateToken(): string {
    // Generate a random 10-digit number
    const min = 1000000000; // Smallest 10-digit number
    const max = 9999999999; // Largest 10-digit number
    const token = Math.floor(Math.random() * (max - min + 1)) + min;
    return token.toString(); // Convert to string and return
  }
  resetForm() {
    this.formData = {
      ddlRecvBy: 0,
    txtName: '',
    txtPhone: '',
    txtAddress:'',
    txtDocument: '',
    ddlDistrict: '0',
    ddlBlock: '0',
    ddlPanchayat: '0',
    ddlVillage: '0',
    ddlComplaintCategory: '0',
    ddlSubCategory: '0',
    txtDetailsE: '',
    ddlComplainttype:'0'
    };
    this.fileToUpload = null;
    this.setCurrentDate();
  }
}
