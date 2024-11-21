import { Component } from '@angular/core';
declare let $: any;

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.scss']
})
export class ViewComponent {

  isPanelOpen = false; // Start with the panel open

  togglePanel() {
    this.isPanelOpen = !this.isPanelOpen; // Toggle the panel state
  }

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
  }
}
