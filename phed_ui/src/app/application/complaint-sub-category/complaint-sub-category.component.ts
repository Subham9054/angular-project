import { Component } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { error } from 'jquery';
import { ValidatorChecklistService } from 'src/app/services/validator-checklist.service';
import { LoadingService } from 'src/app/loading.service';
import { AlertHelper } from 'src/app/core/helper/alert-helper';
declare var $: any;

@Component({
  selector: 'app-complaint-sub-category',
  templateUrl: './complaint-sub-category.component.html',
  styleUrls: ['./complaint-sub-category.component.scss']
})
export class ComplaintSubCategoryComponent {

  categories: any[] = [];
  priorities: any[] = [];
  viewsubcatdata: any[] = [];
  updatesubcatdata: any[] = [];
  isUpdateMode: boolean = false;


  formData: any = {
    escallabel: '',
    complaintsub: '',
    complaintsubhn: '',
    ddlDistrict: '0',
    ddlBlock: '0',
    ddlPanchayat: '0',
    ddlComplaintCategory: [],
    ddlcomplaintPriority: [],
    ddlComplaintsts: '0',
    ddlComplainttype: '0',
    inT_SUB_CATEGORY_ID: ''
  };
  constructor(private http: HttpClient,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    public vldChkLst: ValidatorChecklistService,
    private loadingService: LoadingService,
    private alertHelper: AlertHelper,
  ) { }
  ngOnInit() {
    this.getCategories();
    this.getPriorities();

    this.route.queryParams.subscribe(params => {
      debugger;
      if (params['catid'] && params['subcatid']) {
        this.isUpdateMode = true;
        const catid = params['catid'];
        const subcatid = params['subcatid'];

        // Log parameters for debugging
        console.log('Category ID:', catid, 'Sub-category ID:', subcatid);

        // Fetch categories and priorities
        this.getCategories();
        this.getPriorities();

        // Fetch subcategory data after dropdowns are loaded
        this.authService.UpdateSubCategory(catid, subcatid).subscribe(
          data => {
            this.updatesubcatdata = data;
            console.log('Fetched subcategory data:', this.updatesubcatdata);

            this.bindUpdateData(); // Bind the fetched data
          },
          error => {
            console.error('Error fetching subcategory data:', error);
          }
        );
      }
    });


  }

  getCategories(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.authService.getCategories().subscribe(
        response => {
          this.categories = response;
          resolve();
        },
        error => {
          console.error('Error fetching categories', error);
          reject(error);
        }
      );
    });
  }

  getPriorities(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.authService.getPriorities().subscribe(
        response => {
          this.priorities = response;
          resolve();
        },
        error => {
          console.error('Error fetching priorities', error);
          reject(error);
        }
      );
    });
  }
  bindUpdateData() {
    console.log(this.updatesubcatdata);
    if (this.updatesubcatdata && this.updatesubcatdata.length > 0) {
      const escalation = this.updatesubcatdata[0];

      // Bind dropdown values using their IDs
      this.formData.ddlComplaintCategory = escalation.inT_CATEGORY_ID; // Use the ID for categories

      this.formData.ddlcomplaintPriority = escalation.inT_COMPLAINT_PRIORITY; // Use the ID for priorities

      // Bind other form fields
      this.formData.complaintsubhn = escalation.nvcH_SUB_CATEGORY;
      this.formData.inT_SUB_CATEGORY_ID = escalation.inT_SUB_CATEGORY_ID;
      this.formData.complaintsub = escalation.vcH_SUB_CATEGORY;
      this.formData.escallabel = escalation.inT_ESCALATION_LEVEL;
      console.log('Form data after binding:', this.formData);
    }
  }

  // bindUpdateData() {
  //   debugger;
  //   if (this.updatesubcatdata && this.updatesubcatdata.length > 0) {
  //     const escalation = this.updatesubcatdata[0];
  //     this.formData.ddlComplaintCategory = escalation.vcH_CATEGORY;
  //     this.formData.ddlcomplaintPriority = escalation.vcH_COMPLIANT_PRORITY;
  //     this.formData.complaintsubhn = escalation.nvcH_SUB_CATEGORY;
  //     this.formData.complaintsub = escalation.vcH_SUB_CATEGORY;
  //     this.formData.escallabel = escalation.inT_ESCALATION_LEVEL;


  //   }
  // }

  submitSubcat() {
    let errFlag = 0;
    let INT_CATEGORY_ID = this.formData.ddlComplaintCategory;
    let complaintsub = this.formData.complaintsub;
    let complaintsubhn = this.formData.complaintsubhn;
    let INT_COMPLAINT_PRIORITY = this.formData.ddlcomplaintPriority;
    let escallabel = this.formData.escallabel;
    if (errFlag == 0 &&
      !this.vldChkLst.selectDropdown(INT_CATEGORY_ID, `Complaint Category`)
    ) {
      errFlag = 1;
    }
    if (
      errFlag == 0 && !this.vldChkLst.blankCheck(complaintsub, `Complaint Sub-Category can not be blank`, 'complaintsub')
    ) {
      errFlag = 1;
    }
    if (errFlag == 0 && !this.vldChkLst.isSpecialCharKey('complaintsub', complaintsub,
      `Complaint Sub-Category`)
    ) {
      errFlag = 1;
    }
    if (errFlag == 0 && !this.vldChkLst.maxLength('complaintsub', complaintsub, 50,
      `Complaint Sub-Category`)
    ) {
      errFlag = 1;
    }

    if (
      errFlag == 0 && !this.vldChkLst.blankCheck(complaintsubhn, `Complaint Sub-Category (In Hindi) can not be blank`, 'complaintsubhn')
    ) {
      errFlag = 1;
    }
    if (errFlag == 0 && !this.vldChkLst.isSpecialCharKey('complaintsubhn', complaintsubhn,
      `Complaint Sub-Category (In Hindi)`)
    ) {
      errFlag = 1;
    }
    if (errFlag == 0 && !this.vldChkLst.maxLength('complaintsubhn', complaintsubhn, 50,
      `Complaint Sub-Category (In Hindi)`)
    ) {
      errFlag = 1;
    }

    if (errFlag == 0 &&
      !this.vldChkLst.selectDropdown(INT_COMPLAINT_PRIORITY, `Complaint Priority`)
    ) {
      errFlag = 1;
    }
    if (
      errFlag == 0 && !this.vldChkLst.blankCheck(escallabel, `Escalation Level can not be blank`, 'escallabel')
    ) {
      errFlag = 1;
    }
    if (errFlag == 0 && escallabel > 0 && !this.vldChkLst.maxLength('escallabel', escallabel, 1,
      `Escalation Level`)
    ) {
      errFlag = 1;
    }


    if (errFlag == 0) {
      const registrationData = {
        INT_CATEGORY_ID: this.formData.ddlComplaintCategory,
        VCH_SUB_CATEGORY: this.formData.complaintsub,
        NVCH_SUB_CATEGORY: this.formData.complaintsubhn,
        INT_COMPLAINT_PRIORITY: this.formData.ddlcomplaintPriority,
        INT_ESCALATION_LEVEL: parseInt(this.formData.escallabel, 10)
      };
      this.loadingService.startLoading();

      this.authService.submitSubcategory(registrationData).subscribe(
        (response) => {
          if (response) {
            this.loadingService.stopLoading();
            this.alertHelper.successAlert('Complaint Subcategory submitted successfully', "Success", "success");
          }

          //window.location.reload();
        },
        (error) => {
          this.loadingService.stopLoading();
          this.alertHelper.errorAlert('Failed to submit the complaint Subcategory.', "Error");
        }
      );
    }
  }



  updateSubcat() {
    debugger;
    const subcatElement = document.getElementById('inpthidn') as HTMLInputElement;
    const subcatid = parseInt(subcatElement.value);
    const registrationData = {
      INT_CATEGORY_ID: this.formData.ddlComplaintCategory,
      VCH_SUB_CATEGORY: this.formData.complaintsub,
      NVCH_SUB_CATEGORY: this.formData.complaintsubhn,
      INT_COMPLAINT_PRIORITY: this.formData.ddlcomplaintPriority,
      INT_ESCALATION_LEVEL: this.formData.escallabel
    };
    console.log(registrationData);
    this.authService.updateSubCat(subcatid, registrationData).subscribe(
      (response) => {
        console.log('Subcategory updated successfully:', response);
        alert('Subcategory updated successfully');
        // Navigate or reload if needed
        this.router.navigate(['/application/ComplaintSub-Category']);
      },
      (error) => {
        console.error('Error updating subcategory:', error);
        alert('Failed to update subcategory. Please try again.');
      }
    );
  }



}
