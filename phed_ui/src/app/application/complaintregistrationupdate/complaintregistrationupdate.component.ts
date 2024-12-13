import { Component, OnInit } from '@angular/core';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { error } from 'jquery';
import { AuthService } from 'src/app/auth.service';
declare let $: any;
@Component({
  selector: 'app-complaintregistrationupdate',
  templateUrl: './complaintregistrationupdate.component.html',
  styleUrls: ['./complaintregistrationupdate.component.scss'],
})
export class ComplaintregistrationupdateComponent implements OnInit {

  // Filter close btn
  isDropdownOpen = false;
  openDropdown() {
    this.isDropdownOpen = true;
  }


  closeDropdown() {
    this.isDropdownOpen = false;
  }

  isPanelOpen = false; // Start with the panel open

  togglePanel() {
    this.isPanelOpen = !this.isPanelOpen; // Toggle the panel state
  }
  registrationDate: string = '';
  toDate: string = '';
  districts: any[] = [];
  blocks: any[] = [];
  gps: any[] = [];
  categories: any[] = [];
  subcategories: any[] = [];
  complaintstatus: any[] = [];
  complainttype: any[] = [];
  complaintdetails: any[] = [];
  currentPage: number = 1;
  pageSize: number = 10;
  totalPages: number = 1;
  paginatedComplaints: any[] = [];
  takeactiongms: any[] = [];


  formData: any = {
    ddlDistrict: '0',
    ddlBlock: '0',
    ddlPanchayat: '0',
    ddlComplaintCategory: '0',
    ddlSubCategory: '0',
    ddlComplaintsts: '0',
    ddlComplainttype: '0'
  };

  constructor(private authService: AuthService) { }

  // ngOnInit(): void {
  //   this.initializeData();
  //   $('.datepicker').datetimepicker({
  //     format: 'DD-MMM-YYYY',
  //     daysOfWeekDisabled: [0, 6],
  //   });
  // }
  ngOnInit(): void {

    $('.datepicker').datetimepicker({
      format: 'DD-MMM-YYYY',
      daysOfWeekDisabled: [0, 6],
    });
    $('.timepicker').datetimepicker({
      format: 'LT',
      daysOfWeekDisabled: [0, 6],
    });
    $('.datetimepicker').datetimepicker({
      format: 'DD-MMM-YYYY LT',
      daysOfWeekDisabled: [0, 6],
    });
    const today = new Date();
    //this.updateDates(today);
    this.getDistricts();
    this.getCategories();
    this.getComplaints();
    this.getComplaintstype();
    this.getgmsComplaintdelail();

  }
  private initializeData(): void {
    const today = new Date();
    //this.updateDates(today);
    this.getDistricts();
    this.getCategories();
    this.getComplaints();
    this.getComplaintstype();
    this.getgmsComplaintdelail();

  }
  dataBasedOnToken: any;
  intimations: any;
  actions: any;
  escalations: any;
  GetAllDetailsagainsttokenurl(categoryId: any, subCategoryId: any, Token: any) {
    console.log(Token);

    this.authService.GetAllDetailsagainsttokenurlWithToken(Token, categoryId, subCategoryId).subscribe(
      response => {
        this.dataBasedOnToken = response;
        console.log(this.dataBasedOnToken);
        this.intimations = this.dataBasedOnToken.data[0].intimations;
        this.actions = this.dataBasedOnToken.data[0].actionSummaries;
        this.escalations = this.dataBasedOnToken.data[0].escalations;

      },
      error => {
        console.error('Error fetching Complaint details', error);
      }
    );
  }


  getgmsComplaintdelail() {
    this.authService.getgmsComplaintdelail().subscribe(
      response => {
        this.complaintdetails = response; // Assign full data
        this.totalPages = Math.ceil(this.complaintdetails.length / this.pageSize); // Calculate total pages
        this.updatePagination(); // Initialize pagination
      },
      error => {
        console.error('Error fetching Complaint details', error);
      }
    );
  }
  updatePagination(): void {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.paginatedComplaints = this.complaintdetails.slice(startIndex, endIndex);
    console.log(this.paginatedComplaints);

  }
  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePagination();
    }
  }
  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updatePagination();
    }
  }

  // Method to go to the previous page
  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePagination();
    }
  }

  takeaction(tokenno: string): void {
    debugger;
    alert(tokenno);
    this.authService.getgmstakeaction(tokenno).subscribe(
      response => {
        this.takeactiongms = response;
        console.log('Data fetched successfully:', response); // Optional: For debugging purposes
      },
      error => {
        console.error('Error fetching Complaint status:', error);
        // Optional: Display error to the user, e.g., through a toast or alert.
        alert('Failed to fetch complaint status. Please try again.');
      }
    );
  }



  getComplaints(): void {
    this.authService.getcomplaintstatus().subscribe(
      response => {
        this.complaintstatus = response;
      },
      error => {
        console.error('Error fetching Complaint status', error);
      }
    );
  }

  getComplaintstype(): void {
    this.authService.getcomplainttype().subscribe(
      response => {
        this.complainttype = response;
      },
      error => {
        console.error('Error fetching Complaint types', error);
      }
    );
  }

  getCategories(): void {
    this.authService.getCategories().subscribe(
      response => {
        this.categories = response;
      },
      error => {
        console.error('Error fetching categories', error);
      }
    );
  }

  onCategoryChange(event: any): void {
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

  getDistricts(): void {
    this.authService.getDistricts().subscribe(
      response => {
        this.districts = response;
      },
      error => {
        console.error('Error fetching districts', error);
      }
    );
  }

  onDistrictChange(event: any): void {
    const distId = parseInt(event.target.value, 10);
    if (!isNaN(distId)) {
      this.authService.getBlocks(distId).subscribe(
        response => {
          this.blocks = response;
          this.gps = []; // Reset GPS when district changes
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
      console.error('Invalid block ID');
    }
  }

}
