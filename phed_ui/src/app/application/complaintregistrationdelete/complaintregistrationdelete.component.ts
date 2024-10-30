import { Component } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
declare let $: any;
@Component({
  selector: 'app-complaintregistrationdelete',
  templateUrl: './complaintregistrationdelete.component.html',
  styleUrls: ['./complaintregistrationdelete.component.scss']
})
export class ComplaintregistrationdeleteComponent {
  registrationDate: string = '';
  toDate: string = '';
  districts: any[] = [];
  blocks: any[] = [];
  gps: any[] = [];
  categories: any[] = [];
  subcategories: any[] = [];
  complaintstatus: any[] = [];
  complainttype: any[] = [];

  formData: any = {
    ddlDistrict: '0',
    ddlBlock: '0',
    ddlPanchayat: '0',
    ddlComplaintCategory: '0',
    ddlSubCategory: '0',
    ddlComplaintsts: '0',
    ddlComplainttype: '0'
  };

  constructor(private authService: AuthService) {}

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

  }

  private initializeData(): void {
    const today = new Date();
    this.getDistricts();
    this.getCategories();
    this.getComplaints();
    this.getComplaintstype();
    
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

  onBlockChange(event: any): void {
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
    } else {
      console.error('Invalid district or block ID');
    }
  }

  // isSearchBoxOpen: boolean = false; // Initially, the search box is closed.

  // toggleSearchBox() {
  //   this.isSearchBoxOpen = !this.isSearchBoxOpen; // Toggle the visibility of the search box.
  // }

  
}
