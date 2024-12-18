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

  selectedBlocks: string[] = [];

  selectedInSecondList: { inT_BLOCK_ID: string, vcH_BLOCK_NAME: string }[] = [];
  districts: any[] = [];
  gps: any[] = [];
  blocks: any[] = [];
  villages: any[] = [];
  designations: any[] = [];
  demographymap: FormGroup;
  circle: any;
  division: any;
  subDivision: any;
  section: any;


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
    this.getCircle();

  }

  moveSelectedToSecond() {
    this.selectedBlocks.forEach(selectedValue => {
      const index = this.blocks.findIndex(block => block.inT_BLOCK_ID === selectedValue);
      if (index !== -1) {
        this.selectedInSecondList.push(this.blocks[index]);
        this.blocks.splice(index, 1);
      }
    });
    this.selectedBlocks = [];

  }
  moveAllToSecond() {

    this.selectedBlocks.forEach(selectedValue => {
      const index = this.selectedInSecondList.findIndex(block => block.inT_BLOCK_ID === selectedValue);
      if (index !== -1) {
        this.blocks.push(this.selectedInSecondList[index]);
        this.selectedInSecondList.splice(index, 1);
      }
    });
    this.selectedBlocks = [];
  }

  // moveAllToSecond() {
  //   this.selectedInSecondList.push(...this.blocks);
  //   this.blocks = [];
  // }

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

  getCircle() {
    this.authService.GetCircle().subscribe(
      response => {
        this.circle = response.data;
        console.log(this.circle);


      },
      error => {
        console.error('Error fetching districts', error);
      }
    );
  }

  onDistrictChange(event: any) {
    this.blocks = [];
    this.selectedInSecondList = [];

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

  onCircleChange(event: any) {
    const circleId = event.circleId
    if (!isNaN(circleId)) {
      this.authService.GetDivision(circleId).subscribe(
        response => {
          this.division = response.data;

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

  onDivisionChange(event: any) {
    const divisionId = event.divisionId
    if (!isNaN(divisionId)) {
      this.authService.GetSubDivision(divisionId).subscribe(
        response => {
          this.subDivision = response.data;

          console.log(this.subDivision);

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

  onSubDivisionChange(event: any) {
    const subDivisionId = event.subDivisionId
    if (!isNaN(subDivisionId)) {
      this.authService.GetSection(subDivisionId).subscribe(
        response => {
          this.section = response.data;
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
