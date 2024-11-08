import { Component } from '@angular/core';

@Component({
  selector: 'app-pagecontent-view',
  templateUrl: './pagecontent-view.component.html',
  styleUrls: ['./pagecontent-view.component.scss']
})
export class PagecontentViewComponent {
  isPanelOpen = false; // Start with the panel open

  togglePanel() {
    this.isPanelOpen = !this.isPanelOpen; // Toggle the panel state
  }
}
