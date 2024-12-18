import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth.service';
import { CommonserviceService } from 'src/app/services/commonservice.service';
import { ValidatorChecklistService } from 'src/app/services/validator-checklist.service';


@Component({
  selector: 'app-demographymapping',
  templateUrl: './demographymapping.component.html',
  styleUrls: ['./demographymapping.component.scss']
})
export class DemographymappingComponent {
  // Original list of blocks
  // blocks = [
  //   { inT_BLOCK_ID: '1', vcH_BLOCK_NAME: 'ARARIA' },
  //   { value: '2', name: 'ARWAL' },
  //   { value: '3', name: 'AURANGABAD' }
  // ];

  // Bind selected items from the first select
  selectedBlocks: string[] = [];

  // Bind items moved to the second select
  selectedInSecondList: { inT_BLOCK_ID: string, vcH_BLOCK_NAME: string }[] = [];
  districts: any[] = [];
  gps: any[] = [];
  blocks: any[] = [];
  villages: any[] = [];
  designations: any[] = [];
  demographymap: FormGroup;


  constructor(private http: HttpClient,
    private authService: AuthService,
    private router: Router,
    private fb: FormBuilder,
    public vldChkLst: ValidatorChecklistService,
    public commonService: CommonserviceService,


  ) {
    this.demographymap = this.fb.group({

      district: [null]


    });
  }
  ngOnInit(): void {
    this.getDistricts();
  }

  // moveSelectedToSecond() {
  //   // Move selected items from the first select to the second
  //   this.selectedBlocks.forEach(selectedValue => {
  //     const index = this.blocks.findIndex(block => block.value === selectedValue);
  //     if (index !== -1) {
  //       this.selectedInSecondList.push(this.blocks[index]);
  //       this.blocks.splice(index, 1);
  //     }
  //   });
  //   // Clear selected items after moving
  //   this.selectedBlocks = [];
  // }
  moveSelectedToSecond() {
    // Move selected items from the first select to the second
    this.selectedBlocks.forEach(selectedValue => {
      const index = this.blocks.findIndex(block => block.inT_BLOCK_ID === selectedValue);
      if (index !== -1) {
        this.selectedInSecondList.push(this.blocks[index]);
        this.blocks.splice(index, 1);
      }
    });
    // Clear selected items after moving
    this.selectedBlocks = [];

  }

  moveAllToSecond() {
    // Move all items from the first select to the second
    this.selectedInSecondList.push(...this.blocks);
    this.blocks = [];
  }

  getDistricts() {
    this.authService.getDistricts().subscribe(
      response => {
        this.districts = response;
        console.log(this.districts);

      },
      error => {
        console.error('Error fetching districts', error);
      }
    );
  }
  onDistrictChange(event: any) {
    const distId = event.inT_DIST_ID
    if (!isNaN(distId)) {
      this.authService.getBlocks(distId).subscribe(
        response => {
          this.blocks = response;

          console.log(response);

          this.gps = [];
          this.villages = [];
        },
        error => {
          console.error('Error fetching blocks', error);
        }
      );
    } else {
      console.error('Invalid district ID');
    }
  }
}
