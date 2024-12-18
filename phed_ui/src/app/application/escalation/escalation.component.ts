import { Component, AfterViewInit, AfterViewChecked, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import Swal from 'sweetalert2';
import * as CryptoJS from 'crypto-js';
import { environment } from 'src/environments/environment';
import { error } from 'jquery';


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
  isUpdateMode: boolean = false;
  escalations: any[] = [];
  categoryId: any;
  subCategoryId: any;
  escalationId: any;
  secretKey: any = environment.apiHashingKey;
  selectedEscalations: any = [];
  escalation1: any = [];

  formData: any = {
    ddlComplaintCategory: '0',
    ddlSubCategory: '0',
    ddlDesignation: [],
    ddlLocLevel: [],
    ddlESCALATION_LEVELID: []
  };

  constructor(private http: HttpClient, private authService: AuthService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.getCategories();
    this.getDesignation();
    this.getLocationlevel();


    this.route.queryParams.subscribe(params => {
      if (params['catid'] && params['subcatid'] && params['esclid']) {
        this.isUpdateMode = true;
        const catid = params['catid'];
        const subcatid = params['subcatid'];
        const esclid = params['esclid'];


        // Decrypt the categories if they are passed as encrypted
        this.categoryId = this.decryptData(params['catid'], this.secretKey);
        this.subCategoryId = this.decryptData(params['subcatid'], this.secretKey);
        this.escalationId = this.decryptData(params['esclid'], this.secretKey);




        this.authService.UpdateEscalation(this.categoryId, this.subCategoryId, this.escalationId).subscribe(
          data => {
            this.escalations = data;
            this.bindUpdateData();
            this.viewEscalationDetails(this.categoryId, this.subCategoryId);
          },
          error => {
            console.error('Error fetching escalations:', error);
          }
        );
      }
    });
  }
  // Method to decrypt data
  decryptData(encryptedData: string, secretKey: string): string {
    const bytes = CryptoJS.AES.decrypt(encryptedData, secretKey);
    return bytes.toString(CryptoJS.enc.Utf8);
  }

  bindUpdateData() {
    if (this.escalations && this.escalations.length > 0) {
      const escalation = this.escalations[0];

      // Set location level
      this.formData.ddlComplaintCategory = escalation.inT_CATEGORY_ID;
      this.formData.categoryName = escalation.vcH_CATEGORY;
      this.formData.ddlSubCategory = escalation.inT_SUB_CATEGORY_ID;
      this.formData.subcategoryName = escalation.vcH_SUB_CATEGORY;
      this.escalationLevel = escalation.inT_ESCALATION_LEVELID;

      for (let i = 0; i < this.escalations.length; i++) {
        // Set Designation and Location Level based on escalation data
        this.formData.ddlDesignation[i] = this.escalations[i].inT_DESIG_ID;
        this.formData.ddlLocLevel[i] = this.escalations[i].inT_LEVEL_ID;
      }
      this.generateRows();
    }
  }

  Checked(event: Event) {
    const catdropdown = this.formData.ddlComplaintCategory;
    const subcatdropdown = this.formData.ddlSubCategory;

    if (!catdropdown || !subcatdropdown) {
      alert("Please select both category and subcategory.");
      return;
    }
    this.authService.checkEscalation(catdropdown, subcatdropdown).subscribe(
      (response) => {
        if (response) {
          Swal.fire({
            icon: 'warning',
            title: 'Warning!',
            text: 'Escalation level has already been added for this type!',
          });
          return false;
        }
        return true;
      },
      (error) => {
        console.error('Error checking escalation:', error);
      }
    );
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


  getLocationlevel() {
    this.authService.getLocation().subscribe(
      response => {
        this.locationlevels = response;
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
      standardDays: ''
    }));

    // Initialize ddlDesignation and ddlLocLevel arrays with default value of '0' for non-update mode
    if (this.isUpdateMode) {
      this.formData.ddlDesignation = this.rows.map(() => '');
      this.formData.ddlLocLevel = this.rows.map(() => '');
    } else {
      this.formData.ddlDesignation = new Array(this.escalationLevel).fill('0');
      this.formData.ddlLocLevel = new Array(this.escalationLevel).fill('0');
    }

    this.isTimePickerInitialized = false;
  }

  removeRow(index: number) {
    this.rows.splice(index, 1);
    this.formData.ddlDesignation.splice(index, 1);
    this.formData.ddlLocLevel.splice(index, 1);
    this.isTimePickerInitialized = false;
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


  viewEscalationDetails(categoryId: string, subCategoryId: string): void {
    const catid = categoryId;
    const subcatid = subCategoryId;
    this.authService.viewEscalationeye(catid, subcatid).subscribe(
      data => {
        this.escalation1 = data;
        this.formData.ddlDesignation = this.escalation1.map((row: any) => row.inT_DESIG_ID);
        // this.formData.ddlESCALATION_LEVELID = this.escalation1.map((row: any) => row.inT_ESCALATION_LEVELID);

      },
      error => {
        console.error('Error fetching escalations:', error);
      }
    );
  }
  onCategoryChange(event: any) {
    const catid = parseInt(event.target.value, 10);
    if (!isNaN(catid)) {
      this.authService.getsubcategories(catid).subscribe(
        response => {
          this.subcategories = response;
        },
        error => {
          console.error('Error fetching subcategories', error);
        }
      );
    } else {
      console.error('Invalid category ID');
    }
  }

  // submitForm() {
  //   const submissionData: SubmissionData = {
  //     INT_CATEGORY_ID: this.formData.ddlComplaintCategory,
  //     INT_SUB_CATEGORY_ID: this.formData.ddlSubCategory,
  //     INT_ESCALATION_LEVELID: this.escalationLevel,
  //     escalationDetails: this.rows.map((row, index) => ({
  //       INT_DESIG_ID: this.formData.ddlDesignation[index],
  //       INT_DESIG_LEVELID: this.formData.ddlLocLevel[index],
  //       VCH_STANDARD_DAYS: (document.getElementById(`hrtext${index}`) as HTMLInputElement).value,
  //       // standardDays: '' // Update if needed
  //       standardDays: (document.getElementById(`hrtext${index}`) as HTMLInputElement).value || '' 
  //     })).filter(detail => detail.INT_DESIG_ID && detail.INT_DESIG_LEVELID && detail.VCH_STANDARD_DAYS)
  //   };

  //   if (submissionData.INT_CATEGORY_ID !== "0" && submissionData.INT_SUB_CATEGORY_ID === "0") {
  //     Swal.fire({
  //       icon: 'warning',
  //       title: 'Oops...',
  //       text: 'Please select a subcategory.',
  //     });
  //     return; // Stop execution if the condition is met
  //   }

  //   if (submissionData.escalationDetails.length === 0) {
  //     alert('No valid escalation details to submit.');
  //     return;
  //   }

  //   this.authService.submitEscalationData(submissionData).subscribe(
  //     response => {
  //       console.log('Data submitted successfully', response);
  //       alert('Data submitted successfully');
  //     },
  //     error => {
  //       console.error('Error submitting data', error);
  //       if (error.error && error.error.errors) {
  //         console.error('Validation Errors:', error.error.errors);
  //         alert('Validation Errors: ' + JSON.stringify(error.error.errors));
  //       } else {
  //         alert('Error submitting data: ' + error.message);
  //       }
  //     }
  //   );
  // }






  submitForm() {
    const submissionData: SubmissionData = {
      INT_CATEGORY_ID: this.formData.ddlComplaintCategory,
      INT_SUB_CATEGORY_ID: this.formData.ddlSubCategory,
      INT_ESCALATION_LEVELID: this.escalationLevel,
      escalationDetails: this.rows.map((row, index) => {
        const standardDaysValue = (document.getElementById(`hrtext${index}`) as HTMLInputElement)?.value;

        // Log the row and standard days value for debugging
        console.log('Row data:', row, 'Standard Days Value:', standardDaysValue);

        return {
          INT_DESIG_ID: this.formData.ddlDesignation[index],
          INT_DESIG_LEVELID: this.formData.ddlLocLevel[index],
          VCH_STANDARD_DAYS: standardDaysValue,
          standardDays: standardDaysValue || ''
        };
      }).filter(detail => {
        // Log each detail before filtering
        console.log('Filtered detail:', detail);
        return detail.INT_DESIG_ID && detail.INT_DESIG_LEVELID && detail.VCH_STANDARD_DAYS;
      })
    };

    console.log('Escalation Details:', submissionData.escalationDetails);

    if (submissionData.INT_CATEGORY_ID !== "0" && submissionData.INT_SUB_CATEGORY_ID === "0") {
      Swal.fire({
        icon: 'warning',
        title: 'Oops...',
        text: 'Please select a subcategory.',
      });
      return; // Stop execution if the condition is met
    }

    if (submissionData.escalationDetails.length === 0) {
      alert('No valid escalation details to submit.');
      return;
    }

  }




}
