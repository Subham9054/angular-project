import { Component } from '@angular/core';
declare let $: any;
@Component({
  selector: 'app-newsevent',
  templateUrl: './newsevent.component.html',
  styleUrls: ['./newsevent.component.scss']
})
export class NewseventComponent {
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
