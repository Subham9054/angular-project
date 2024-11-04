import { Component } from '@angular/core';

@Component({
  selector: 'app-mngmenu',
  templateUrl: './mngmenu.component.html',
  styleUrls: ['./mngmenu.component.scss']
})
export class MngmenuComponent {
// Primary link click Parent Menu felid show
  selectedLinkType: string = '';
  showParentMenu: boolean = false;

  onLinkTypeChange() {
    this.showParentMenu = this.selectedLinkType === '2';
  }
}
