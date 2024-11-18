import { Component } from '@angular/core';

@Component({
  selector: 'app-mngfaq-view',
  templateUrl: './mngfaq-view.component.html',
  styleUrls: ['./mngfaq-view.component.scss']
})
export class MngfaqViewComponent {
// Filter close btn
isDropdownOpen = false;
openDropdown() {
  this.isDropdownOpen = true;
}


closeDropdown() {
  this.isDropdownOpen = false;
}
}
