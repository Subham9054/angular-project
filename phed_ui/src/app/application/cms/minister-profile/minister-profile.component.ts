import { Component } from '@angular/core';

@Component({
  selector: 'app-minister-profile',
  templateUrl: './minister-profile.component.html',
  styleUrls: ['./minister-profile.component.scss']
})
export class MinisterProfileComponent {
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