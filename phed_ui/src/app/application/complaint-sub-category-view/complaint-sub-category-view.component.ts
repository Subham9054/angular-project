import { Component } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
declare let $: any;

@Component({
  selector: 'app-complaint-sub-category-view',
  templateUrl: './complaint-sub-category-view.component.html',
  styleUrls: ['./complaint-sub-category-view.component.scss']
})
export class ComplaintSubCategoryViewComponent {

  categories: any[] = [];
  subcategories: any[] = [];
  
  // Initialize formData with a default value
  formData: any = {
    ddlComplaintCategory: null,  // Default value should be null
  };

  constructor(private http: HttpClient, private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.getCategories();
    // Initialize date/time pickers
    this.initDateTimePickers();
    this.subCatData();
  }

  getCategories() {
    this.authService.getCategories().subscribe(
      response => {
        this.categories = response;
        console.log(this.categories);
      },
      error => {
        console.error('Error fetching categories', error);
        // Optionally, show an alert or message to the user
      }
    );
  }
  subCatData() {
    debugger;
    const catid = this.formData?.ddlComplaintCategory ?? 0;
    const subcatid = this.formData?.ddlSubCategory ?? 0;
  
    this.authService.getComplaintSubCategory(catid, subcatid).subscribe(
      (response) => {
        this.subcategories = response;
        console.log('Subcategories:', this.subcategories);
      },
      (error) => {
        console.error('Error fetching categories', error);
        alert('Unable to fetch subcategories. Please try again later.');
      }
    );
  }
  deleteSubCategory(cateid:any, subcateid:any){
    const catid=cateid;
    //alert(catid);
    const subcatid=subcateid;
    this.authService.deleteComplaintSubCategory(catid, subcatid).subscribe(
      (response) => {
        if(response==1){
          alert("Subcategory Deleted Successfully");
          window.location.reload();
        }
      },
      (error) => {
        alert('Unable to delete subcategories. Please try again later.');
      }
    );
  }

  Updateview(categoryId: string, subCategoryId: string){
    //alert(categoryId);
    //alert(subCategoryId);
    // alert(escalationlevelId);
    const catid=categoryId;
    const subcatid=subCategoryId;
    this.router.navigate(['/application/ComplaintSub-Category/add'], {
      queryParams: {
        catid: categoryId,
        subcatid: subCategoryId,
      }
    });
   
}
  // Initialize the date pickers
  initDateTimePickers() {
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
  searchComplaintCategory(){
    alert('ok');
  }
  // Filter Dropdown methods
  isDropdownOpen = false;

  openDropdown() {
    this.isDropdownOpen = true;
  }

  closeDropdown() {
    this.isDropdownOpen = false;
  }

  // Reset filters
  resetFilters() {
    // Reset formData to null or default value
    this.formData.ddlComplaintCategory = null;
    // Reset any other filters here if necessary
    window.location.reload()
  }
}
