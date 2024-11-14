import { Component } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { error } from 'jquery';
declare var $:any;

@Component({
  selector: 'app-complaint-sub-category',
  templateUrl: './complaint-sub-category.component.html',
  styleUrls: ['./complaint-sub-category.component.scss']
})
export class ComplaintSubCategoryComponent {

  categories: any[] = [];
  priorities: any[] = [];
  viewsubcatdata:any[]=[];


  formData: any = {
    escallabel:'',
    complaintsub:'',
    complaintsubhn :'',
    ddlDistrict: '0',
    ddlBlock: '0',
    ddlPanchayat: '0',
    ddlComplaintCategory: [],
    ddlcomplaintPriority: [],
    ddlComplaintsts: '0',
    ddlComplainttype: '0'
  };
  constructor(private http: HttpClient, private authService: AuthService, private router: Router) {}
  ngOnInit() {
    this.getCategories();
    this.getPriorities();
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

  getPriorities(){
    this.authService.getPriorities().subscribe(
      Response =>{
        this.priorities=Response;
      },
      error=>{
        console.error('Error Fetching Priorities',error);
      }
    )
  }
  submitSubcat() {
    debugger;
    const complaintsub = this.formData.ddlcomplaintPriority;
    alert(complaintsub);
    const registrationData = {
      INT_CATEGORY_ID: this.formData.ddlComplaintCategory ,
      VCH_SUB_CATEGORY: this.formData.complaintsub ,
      NVCH_SUB_CATEGORY:this.formData.complaintsubhn,
      INT_COMPLAINT_PRIORITY: this.formData.ddlcomplaintPriority,
      INT_ESCALATION_LEVEL: this.formData.escallabel
    };

    // Log the payload for debugging
    console.log('Registration Data Payload:', registrationData);

    this.authService.submitSubcategory(registrationData).subscribe(
      (response) => {
        alert('Complaint Subcategory submitted successfully');
        //alert(response); // Show the text response
        //this.resetForm();
        window.location.reload();
      },
      (error) => {
        console.error('Error submitting registration data:', error);
        alert('Failed to submit registration data. Please try again.');
      }
    );
  }
 


}
