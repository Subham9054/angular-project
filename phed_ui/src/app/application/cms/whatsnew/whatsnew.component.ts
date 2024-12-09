import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/auth.service';
import Swal from 'sweetalert2';
import * as moment from 'moment';

declare let $: any;
@Component({
  selector: 'app-whatsnew',
  templateUrl: './whatsnew.component.html',
  styleUrls: ['./whatsnew.component.scss']
})
export class WhatsnewComponent implements OnInit {
  whatisNewModel: any = {
    whatIsNewId: null,
    titleEnglish: '',
    titleHindi: '',
    descriptionEnglish: '',
    descriptionHindi: '',
    isPublish: false,
    publishDate: '',
    createdBy: null,
    isEditing: false,
  };

   // file upload
  fileName: string = '';

  handleFileInput(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.fileName = file.name;
    }
  }

  constructor(private authService: AuthService, private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    const today = moment().format('DD-MMM-YYYY'); // Get today's date in the desired format
    this.whatisNewModel.publishDate = today; // Set today's date in the model

    // Initialize datetimepicker with today's date as the default
    $('.publishDate').datetimepicker({
      format: 'DD-MMM-YYYY',
      daysOfWeekDisabled: [0, 6], // Example: Disable weekends
      defaultDate: moment(), // Set default date to today
    }).on('dp.change', (e: any) => {
      this.whatisNewModel.publishDate = e.date.format('DD-MMM-YYYY'); // Update the model on date change
    });
  }

  resetForm(): void {
    this.whatisNewModel = {
      whatIsNewId: null,
      titleEnglish: '',
      titleHindi: '',
      descriptionEnglish: '',
      descriptionHindi: '',
      isPublish: false,
      publishDate: '',
      createdBy: '',
      isEditing: false,
    };
    this.fileName = '';    
  }
}
