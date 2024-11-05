import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {
  constructor(private router: Router, private route: ActivatedRoute) {}

  isActive(path: string): boolean {
    return this.router.url.includes(path);
  }

  
}
