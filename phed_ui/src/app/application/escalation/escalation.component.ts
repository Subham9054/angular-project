import { Component, AfterViewInit, AfterViewChecked, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import Swal from 'sweetalert2';

declare let $: any;

interface EscalationDetail {
  INT_DESIG_ID: string; // Adjust type as necessary
  INT_DESIG_LEVELID: string; // Adjust type as necessary
  VCH_STANDARD_DAYS: string;
  standardDays: string;
}

interface SubmissionData {
  INT_CATEGORY_ID: string; // Adjust type based on your requirements
  INT_SUB_CATEGORY_ID: string; // Adjust type based on your requirements
  INT_ESCALATION_LEVELID: number;
  escalationDetails: EscalationDetail[]; // Change to EscalationDetail array
}

@Component({
  selector: 'app-escalation',
  templateUrl: './escalation.component.html',
  styleUrls: ['./escalation.component.scss']
})
export class EscalationComponent implements OnInit, AfterViewInit, AfterViewChecked {
  
  escalationLevel: number = 0;
  rows: EscalationDetail[] = []; // Use the EscalationDetail type
  isTimePickerInitialized: boolean = false;
  categories: any[] = [];
  subcategories: any[] = [];
  designations: any[] = [];
  locationlevels: any[] = [];
  
  formData: any = { 
    ddlComplaintCategory: '0',
    ddlSubCategory: '0',
    ddlDesignation: [],
    ddlLocLevel: [],
  };

  constructor(private http: HttpClient, private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.getCategories();
    this.getDesignation();
    this.getLocationlevel();
    //this.Checked();
  }

  Checked(event: Event) {
    // Access values from formData due to two-way binding with ngModel
    const catdropdown = this.formData.ddlComplaintCategory;
    const subcatdropdown = this.formData.ddlSubCategory;

    // Check if values are selected
    if (!catdropdown || !subcatdropdown) {
        alert("Please select both category and subcategory.");
        return;
    }
    this.authService.checkEscalation(catdropdown, subcatdropdown).subscribe(
      (response) => {
          console.log(response);
          // Check if response indicates an existing escalation
          if (response) {
            Swal.fire({
              icon: 'warning',
              title: 'Warning!',
              text: 'Escalation level has already been added for this type!',
            });
            return false;
          }
          // Explicit return value if no escalation level exists
          return true;
      },
      (error) => {
          // Log error details and provide feedback to the user
          console.error('Error checking escalation:', error);
      }
  );  
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

  getLocationlevel() {
    this.authService.getLocation().subscribe(
      response => {
        this.locationlevels = response;
        console.log(this.locationlevels);
      },
      error => {
        console.error('Error fetching locations', error);
      }
    );
  }

  getDesignation() {
    this.authService.getDesignation().subscribe(
      response => {
        this.designations = response;
        console.log(this.designations);
      },
      error => {
        console.error('Error fetching designations', error);
      }
    );
  }

  generateRows() {
    this.rows = Array.from({ length: this.escalationLevel }, () => ({
      INT_DESIG_ID: '',
      INT_DESIG_LEVELID: '',
      VCH_STANDARD_DAYS: '',
      standardDays: '' // Initialize the standardDays property
    }));
    
    // Initialize ddlDesignation and ddlLocLevel arrays
    this.formData.ddlDesignation = new Array(this.escalationLevel).fill('');
    this.formData.ddlLocLevel = new Array(this.escalationLevel).fill('');
    
    this.isTimePickerInitialized = false; // Reset to reinitialize time pickers
  }

  removeRow(index: number) {
    this.rows.splice(index, 1);
    this.formData.ddlDesignation.splice(index, 1); // Remove the corresponding designation
    this.formData.ddlLocLevel.splice(index, 1); // Remove the corresponding location level
    this.isTimePickerInitialized = false; // Reset to reinitialize time pickers
  }

  ngAfterViewInit(): void {
    this.initializeTimePickers();
  }

  ngAfterViewChecked(): void {
    if (!this.isTimePickerInitialized) {
      this.initializeTimePickers();
      this.isTimePickerInitialized = true;
    }
  }

  initializeTimePickers() {
    $('.timepicker').datetimepicker({
      format: 'LT',
      daysOfWeekDisabled: [0, 6]
    });
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
  submitForm() {
    const submissionData: SubmissionData = {
        INT_CATEGORY_ID: this.formData.ddlComplaintCategory,
        INT_SUB_CATEGORY_ID: this.formData.ddlSubCategory,
        
        INT_ESCALATION_LEVELID: this.escalationLevel,
        escalationDetails: this.rows.map((row, index) => ({
            INT_DESIG_ID: this.formData.ddlDesignation[index],
            INT_DESIG_LEVELID: this.formData.ddlLocLevel[index],
            VCH_STANDARD_DAYS: (document.getElementById(`hrtext${index}`) as HTMLInputElement).value,
            standardDays: '' // Update if needed
        })).filter(detail => detail.INT_DESIG_ID && detail.INT_DESIG_LEVELID && detail.VCH_STANDARD_DAYS)
    };
    if (submissionData.INT_CATEGORY_ID !== "0" && submissionData.INT_SUB_CATEGORY_ID === "0") {
      Swal.fire({
        icon: 'warning',
        title: 'Oops...',
        text: 'Please select a subcategory.',
      });
      return; // Stop execution if the condition is met
    }
    // Make sure to check if escalationDetails is not empty before calling the API
    if (submissionData.escalationDetails.length === 0) {
        alert('No valid escalation details to submit.');
        return;
    }

    this.authService.submitEscalationData(submissionData).subscribe(
      
        response => {
            console.log('Data submitted successfully', response);
            alert('Data submitted successfully');
        },
        error => {
            console.error('Error submitting data', error);
            if (error.error && error.error.errors) {
                console.error('Validation Errors:', error.error.errors);
                alert('Validation Errors: ' + JSON.stringify(error.error.errors));
            } else {
                alert('Error submitting data: ' + error.message);
            }
        }
    );
}

}
