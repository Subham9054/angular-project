import { Component } from '@angular/core';
declare let $: any;
@Component({
  selector: 'app-modewise-report',
  templateUrl: './modewise-report.component.html',
  styleUrls: ['./modewise-report.component.scss']
})
export class ModewiseReportComponent {
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
