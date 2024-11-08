import { Component } from '@angular/core';

@Component({
  selector: 'app-complaintregistration-view',
  templateUrl: './complaintregistration-view.component.html',
  styleUrls: ['./complaintregistration-view.component.scss']
})
export class ComplaintregistrationViewComponent {
  isPanelOpen = false; // Start with the panel open

  togglePanel() {
    this.isPanelOpen = !this.isPanelOpen; // Toggle the panel state
  }
}
