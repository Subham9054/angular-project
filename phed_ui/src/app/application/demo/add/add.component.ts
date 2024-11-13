import { Component } from '@angular/core';
import { Router } from '@angular/router';
declare let $: any;
@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent {
  constructor(private router: Router ) { }

// Select upload
// options = [
//   { id: 1, name: 'Option 1' },
//   { id: 2, name: 'Option 2' },
//   { id: 3, name: 'Option 3' }
// ];


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
  // file upload
  fileName: string = '';

  handleFileInput(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.fileName = file.name;
    }
  }
  
}
