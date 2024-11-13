import { Component } from '@angular/core';

@Component({
  selector: 'app-mnggallery-view',
  templateUrl: './mnggallery-view.component.html',
  styleUrls: ['./mnggallery-view.component.scss']
})
export class MnggalleryViewComponent {
  isPanelOpen = false; // Start with the panel open

  togglePanel() {
    this.isPanelOpen = !this.isPanelOpen; // Toggle the panel state
  }
}
