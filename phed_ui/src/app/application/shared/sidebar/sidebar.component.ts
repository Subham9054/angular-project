import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {
  cmnmst: any;
  cms: any;
  config: any;
  gms: any;
  isMisReport: any;

  ngOnInit(): void {
    this.cmnmst = sessionStorage.getItem('cmnmst');
    this.cms = sessionStorage.getItem('cms');
    this.config = sessionStorage.getItem('config');
    this.gms = sessionStorage.getItem('gms');
    this.isMisReport = sessionStorage.getItem('isMisReport');


  }



}
