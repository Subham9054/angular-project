import { Component } from '@angular/core';

@Component({
  selector: 'app-mngmenu-view',
  templateUrl: './mngmenu-view.component.html',
  styleUrls: ['./mngmenu-view.component.scss']
})
export class MngmenuViewComponent {
  isPanelOpen = false; // Start with the panel open

  togglePanel() {
    this.isPanelOpen = !this.isPanelOpen; // Toggle the panel state
  }
}
