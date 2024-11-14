import { Component } from '@angular/core';
declare let $: any;
@Component({
  selector: 'app-complaint-sub-category-view',
  templateUrl: './complaint-sub-category-view.component.html',
  styleUrls: ['./complaint-sub-category-view.component.scss']
})
export class ComplaintSubCategoryViewComponent {
   // Filter 
   isDropdownOpen = false;
   openDropdown() {
     this.isDropdownOpen = true;
   }
  
  
   closeDropdown() {
     this.isDropdownOpen = false;
   }

   resetFilters() {
    // Reset form fields logic if needed
  }

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
// Select upload
items = [
  { id: 1, name: 'Handpump Tubewell Related' },
  { id: 2, name: 'Miscellaneous' },
  { id: 3, name: 'Piped Water Supply Related' },
  { id: 4, name: 'Water Quality Related' },
];
selectedItem: any;
}
