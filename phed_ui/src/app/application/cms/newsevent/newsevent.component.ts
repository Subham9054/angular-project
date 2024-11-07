import { Component } from '@angular/core';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
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
    // Ck editor
    editor = ClassicEditor;
    data: any = `<p class="text-grey">Enter here...</p>`;
  // image upload

  files: File[] = [];

	onSelect(event:any) {
		console.log(event);
		this.files.push(...event.addedFiles);
	}

	onRemove(event:any) {
		console.log(event);
		this.files.splice(this.files.indexOf(event), 1);
	}
}
