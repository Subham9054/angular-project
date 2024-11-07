import { Component, OnInit } from '@angular/core';
import { HomeService } from '../services/home.service';
import { TranslationService } from '../services/translation.service';

interface Complaint {
  complaintCategoryId: number;
  complaintCategoryNameEng: string;
  complaintCategoryNameHin: string;
  complaintSubCategoryId: number;
  complaintSubCategoryNameEng: string;
  complaintSubCategoryNameHin: string;
  showCategory?: boolean;
  slNo?: string;
}

@Component({
  selector: 'app-complaint',
  templateUrl: './complaint.component.html',
  styleUrls: ['./complaint.component.scss']
})
export class ComplaintComponent implements OnInit {
  complaintDisplayList: Complaint[] = [];
  currentLanguage: string = '';

  constructor(
    private homeService: HomeService,
    private translationService: TranslationService
  ) {}

  ngOnInit(): void {
    this.currentLanguage = this.translationService.getLanguage();
    this.fetchComplaints();

    // Update the current language if it changes
    this.translationService.onLanguageChange().subscribe((event) => {
      this.currentLanguage = event.lang;
      this.updateSerialNumbers();
    });
  }

  fetchComplaints(): void {
    this.homeService.complaintDetails().subscribe(
      (response: any) => {
        if (response.success && response.data) {
          this.organizeComplaints(response.data);
        }
      },
      (error) => {
        console.error('Error fetching complaints:', error);
      }
    );
  }

  organizeComplaints(data: Complaint[]): void {
    const categorySet = new Set<number>();
    let serialNumber = 1;

    data.forEach((complaint) => {
      if (!categorySet.has(complaint.complaintCategoryId)) {
        complaint.showCategory = true;
        complaint.slNo = this.convertToHindiNumerals(serialNumber++);
        categorySet.add(complaint.complaintCategoryId);
      } else {
        complaint.showCategory = false;
      }
      this.complaintDisplayList.push(complaint);
    });
  }

  convertToHindiNumerals(num: number): string {
    if (this.currentLanguage === 'hi') {
      const hindiNumbers = ['०', '१', '२', '३', '४', '५', '६', '७', '८', '९'];
      return num.toString().split('').map(digit => hindiNumbers[+digit]).join('');
    }
    return num.toString();
  }

  updateSerialNumbers(): void {
    let serialNumber = 1;
    this.complaintDisplayList.forEach((complaint) => {
      if (complaint.showCategory) {
        complaint.slNo = this.convertToHindiNumerals(serialNumber++);
      }
    });
  }

  getRowSpan(data: Complaint[], index: number): number {
    const categoryId = data[index].complaintCategoryId;
    return data.filter((c) => c.complaintCategoryId === categoryId).length;
  }
}
