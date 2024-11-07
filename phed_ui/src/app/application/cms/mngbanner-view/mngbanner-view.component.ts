import { Component } from '@angular/core';

@Component({
  selector: 'app-mngbanner-view',
  templateUrl: './mngbanner-view.component.html',
  styleUrls: ['./mngbanner-view.component.scss']
})
export class MngbannerViewComponent {
  isPanelOpen = true; // Start with the panel open

  togglePanel() {
    this.isPanelOpen = !this.isPanelOpen; // Toggle the panel state
  }
}
