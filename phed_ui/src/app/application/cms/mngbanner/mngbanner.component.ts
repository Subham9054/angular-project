import { Component } from '@angular/core';

@Component({
  selector: 'app-mngbanner',
  templateUrl: './mngbanner.component.html',
  styleUrls: ['./mngbanner.component.scss']
})
export class MngbannerComponent {
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
