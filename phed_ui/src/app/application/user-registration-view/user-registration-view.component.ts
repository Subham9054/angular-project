import { Component } from '@angular/core';

@Component({
  selector: 'app-user-registration-view',
  templateUrl: './user-registration-view.component.html',
  styleUrls: ['./user-registration-view.component.scss']
})
export class UserRegistrationViewComponent {
// Filter close btn
isDropdownOpen = false;
openDropdown() {
  this.isDropdownOpen = true;
}


closeDropdown() {
  this.isDropdownOpen = false;
}
}
