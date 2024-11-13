import { Component } from '@angular/core';
declare let $: any;
@Component({
  selector: 'app-complaint-sub-category-view',
  templateUrl: './complaint-sub-category-view.component.html',
  styleUrls: ['./complaint-sub-category-view.component.scss']
})
export class ComplaintSubCategoryViewComponent {
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
