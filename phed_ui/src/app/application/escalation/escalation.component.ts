import { Component } from '@angular/core';
declare let $: any;

@Component({
  selector: 'app-escalation',
  templateUrl: './escalation.component.html',
  styleUrls: ['./escalation.component.scss']
})
export class EscalationComponent {

  escalationLevel: number = 0;
  rows: any[] = [];
  isTimePickerInitialized: boolean = false;

  generateRows() {
    this.rows = Array(this.escalationLevel).fill({});
    this.isTimePickerInitialized = false; // Reset to reinitialize time pickers
  }

  removeRow(index: number) {
    this.rows.splice(index, 1);
    this.isTimePickerInitialized = false; // Reset to reinitialize time pickers
  }

  ngAfterViewInit(): void {
    this.initializeTimePickers();
  }

  ngAfterViewChecked(): void {
    // Reinitialize time pickers if they haven't been initialized for the current view
    if (!this.isTimePickerInitialized) {
      this.initializeTimePickers();
      this.isTimePickerInitialized = true;
    }
  }

  initializeTimePickers() {
    // Initialize time pickers with jQuery
    $('.timepicker').datetimepicker({
      format: 'LT',
      daysOfWeekDisabled: [0, 6]
    });
  }

















  // // Escalation felid
  // escalationLevel: number = 0;
  // rows: any[] = [];

  // generateRows() {
  //   this.rows = Array(this.escalationLevel).fill({});
  // }

  // removeRow(index: number) {
  //   this.rows.splice(index, 1);
  // }

  // // Date and time picker
  // ngOnInit(): void {
  //   $('.timepicker').datetimepicker({
  //     format: 'LT',
  //     daysOfWeekDisabled: [0, 6],
  //   });
  // }



}
