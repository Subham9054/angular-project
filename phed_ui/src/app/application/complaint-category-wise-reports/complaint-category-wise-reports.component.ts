import { Component } from '@angular/core';
declare let $: any;
@Component({
  selector: 'app-complaint-category-wise-reports',
  templateUrl: './complaint-category-wise-reports.component.html',
  styleUrls: ['./complaint-category-wise-reports.component.scss']
})
export class ComplaintCategoryWiseReportsComponent {

 // Filter close btn
 isDropdownOpen = false;
 openDropdown() {
   this.isDropdownOpen = true;
 }
 closeDropdown() {
  this.isDropdownOpen = false;
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

}
