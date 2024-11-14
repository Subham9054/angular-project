import { Component } from '@angular/core';

@Component({
  selector: 'app-complaint-sub-category',
  templateUrl: './complaint-sub-category.component.html',
  styleUrls: ['./complaint-sub-category.component.scss']
})
export class ComplaintSubCategoryComponent {
 // Select upload
items = [
  { id: 1, name: 'Handpump Tubewell Related' },
  { id: 2, name: 'Miscellaneous' },
  { id: 3, name: 'Piped Water Supply Related' },
  { id: 4, name: 'Water Quality Related' },
];
selectedItem: any;


 // Select upload
 item = [
  { id: 1, name: 'High' },
  { id: 2, name: 'Low' },
  { id: 3, name: 'Medium' },
];
selectedItem1: any;


}
