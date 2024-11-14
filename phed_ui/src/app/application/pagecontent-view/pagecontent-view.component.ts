import { Component } from '@angular/core';

@Component({
  selector: 'app-pagecontent-view',
  templateUrl: './pagecontent-view.component.html',
  styleUrls: ['./pagecontent-view.component.scss']
})
export class PagecontentViewComponent {
  // Filter close btn
  isDropdownOpen = false;
  openDropdown() {
    this.isDropdownOpen = true;
  }
 
 
  closeDropdown() {
    this.isDropdownOpen = false;
  }
}
