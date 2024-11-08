import { Component } from '@angular/core';
declare let $: any;
@Component({
  selector: 'app-whatnew-view',
  templateUrl: './whatnew-view.component.html',
  styleUrls: ['./whatnew-view.component.scss']
})
export class WhatnewViewComponent {
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
// filter tab change
isPanelOpen = false; // Start with the panel open

togglePanel() {
  this.isPanelOpen = !this.isPanelOpen; // Toggle the panel state
}
}
