import { Component, AfterViewInit, AfterViewChecked, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
declare let $: any;

@Component({
  selector: 'app-escalation',
  templateUrl: './escalation.component.html',
  styleUrls: ['./escalation.component.scss']
})
export class EscalationComponent implements OnInit, AfterViewInit, AfterViewChecked {

  escalationLevel: number = 0;
  rows: any[] = [];
  isTimePickerInitialized: boolean = false;
  categories: any[] = [];
  subcategories: any[] = [];
  designations: any[] = [];
  locationlevels: any[] = [];

  formData: any = { 
    ddlComplaintCategory: '0',
    ddlSubCategory: '0',
    ddlDesignation: [],
    ddlLocLevel: []
  };

  constructor(private http: HttpClient, private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.getCategories();
    this.getDesignation();
    this.getLocationlevel();
  }

  getCategories() {
    this.authService.getCategories().subscribe(
      response => {
        this.categories = response;
        console.log(this.categories);
      },
      error => {
        console.error('Error fetching categories', error);
      }
    );
  }

  getLocationlevel() {
    this.authService.getLocation().subscribe(
      response => {
        this.locationlevels = response;
        console.log(this.locationlevels);
      },
      error => {
        console.error('Error fetching locations', error);
      }
    );
  }

  getDesignation() {
    this.authService.getDesignation().subscribe(
      response => {
        this.designations = response;
        console.log(this.designations);
      },
      error => {
        console.error('Error fetching designations', error);
      }
    );
  }

  generateRows() {
    this.rows = Array(this.escalationLevel).fill({});
    this.formData.ddlDesignation = new Array(this.escalationLevel).fill(0); // Initialize ddlDesignation for each row
    this.formData.ddlLocLevel = new Array(this.escalationLevel).fill(0); // Initialize ddlLocLevel for each row
    this.isTimePickerInitialized = false; // Reset to reinitialize time pickers
  }

  removeRow(index: number) {
    this.rows.splice(index, 1);
    this.formData.ddlDesignation.splice(index, 1); // Remove the corresponding designation
    this.formData.ddlLocLevel.splice(index, 1); // Remove the corresponding location level
    this.isTimePickerInitialized = false; // Reset to reinitialize time pickers
  }

  ngAfterViewInit(): void {
    this.initializeTimePickers();
  }

  ngAfterViewChecked(): void {
    if (!this.isTimePickerInitialized) {
      this.initializeTimePickers();
      this.isTimePickerInitialized = true;
    }
  }

  initializeTimePickers() {
    $('.timepicker').datetimepicker({
      format: 'LT',
      daysOfWeekDisabled: [0, 6]
    });
  }
  

  onCategoryChange(event: any) {
    const catid = parseInt(event.target.value, 10);
    if (!isNaN(catid)) {
      this.authService.getsubcategories(catid).subscribe(
        response => {
          this.subcategories = response;
          console.log(this.subcategories);
        },
        error => {
          console.error('Error fetching subcategories', error);
        }
      );
    } else {
      console.error('Invalid category ID');
    }
  }
}
