import { Component } from '@angular/core';
declare let $: any;
@Component({
  selector: 'app-complaint-category-wise-reports',
  templateUrl: './complaint-category-wise-reports.component.html',
  styleUrls: ['./complaint-category-wise-reports.component.scss']
})
export class ComplaintCategoryWiseReportsComponent {
  // isPanelOpen = false; // Start with the panel open

  // togglePanel() {
  //   this.isPanelOpen = !this.isPanelOpen; // Toggle the panel state
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

  }


}
