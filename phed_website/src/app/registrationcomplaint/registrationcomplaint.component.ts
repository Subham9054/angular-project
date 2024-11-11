import { Component } from '@angular/core';

@Component({
	selector: 'app-registrationcomplaint',
	templateUrl: './registrationcomplaint.component.html',
	styleUrls: ['./registrationcomplaint.component.scss']
})
export class RegistrationcomplaintComponent {

	files: File[] = [];

	ngOnInit(): void {
		
	};

	onSelect(event: any) {
		console.log(event);
		this.files.push(...event.addedFiles);
	}

	onRemove(event: any) {
		console.log(event);
		this.files.splice(this.files.indexOf(event), 1);
	}


}
