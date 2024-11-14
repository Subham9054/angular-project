import { Component } from '@angular/core';

@Component({
  selector: 'app-demographymapping-view',
  templateUrl: './demographymapping-view.component.html',
  styleUrls: ['./demographymapping-view.component.scss']
})
export class DemographymappingViewComponent {

 // Filter close btn
 isDropdownOpen = false;
 openDropdown() {
   this.isDropdownOpen = true;
 }


 closeDropdown() {
   this.isDropdownOpen = false;
 }


  searchTerm: string = '';
  showSearch: boolean = false;

  // Original list of circles
  circles = [
    { id: 1, name: 'Ara' },
    { id: 2, name: 'Begusarai' },
    { id: 3, name: 'Bhagalpur' }
  ];

  // Filtered list based on search term
  get filteredCircles() {
    return this.circles.filter(circle =>
      circle.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

  // Show search bar when typing starts
  onSearchChange() {
    this.showSearch = !!this.searchTerm;
  }
}
