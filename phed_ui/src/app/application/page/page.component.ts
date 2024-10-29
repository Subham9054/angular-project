import { Component } from '@angular/core';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.scss']
})
export class PageComponent {
  isSubMenu: boolean = false;

  toggleSubMenu(value: boolean): void {
    this.isSubMenu = value;
  }
}
