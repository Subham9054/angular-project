import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth.service';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-mngfaq-view',
  templateUrl: './mngfaq-view.component.html',
  styleUrls: ['./mngfaq-view.component.scss']
})
export class MngfaqViewComponent implements OnInit {
  faqs: any[] = [];
  selectedFaq: any = null;
  noRecordsFound: boolean = false;

  constructor(private authService: AuthService, private router: Router, private sanitizer: DomSanitizer) {}

  // Filter close btn
  isDropdownOpen = false;
  openDropdown() {
    this.isDropdownOpen = true;
  }
  closeDropdown() {
    this.isDropdownOpen = false;
  }

  ngOnInit(): void {
    this.loadFAQs();
  }

  // Method to load FAQs
  loadFAQs(): void {
    this.authService.getFAQs().subscribe({
      next: (response: any) => {
        if (response.success) {
          this.faqs = response.data.map((faq: any) => ({
            ...faq,
            faqAnsEng: this.sanitizeHtml(faq.faqAnsEng),
            faqAnsHin: this.sanitizeHtml(faq.faqAnsHin)
          }));
        } else {
          console.error('No FAQs found:', response.Message);
          Swal.fire('Info', 'No FAQs found.', 'info');
        }
      },
      error: (error) => {
        console.error('Error fetching FAQs:', error);
        Swal.fire('Error', 'There was an error fetching the FAQs.', 'error');
      }
    });
  }

  // Sanitize HTML method
  sanitizeHtml(html: string): SafeHtml {
    return this.sanitizer.bypassSecurityTrustHtml(html);
  }

  // Method to handle the Modal View action
  onView(faq: any): void {
    this.selectedFaq = faq;
    // Ensure to sanitize the content when viewing in modal
    this.selectedFaq.faqAnsEng = (faq.faqAnsEng);
    this.selectedFaq.faqAnsHin = (faq.faqAnsHin);
  }

  // Method to handle the Edit action
  onEdit(id: number) {
    this.router.navigate([`/application/mngfaq/${id}`]);
  }

  // onEdit(id: number): void {
  //   this.router.navigate([`/application/mngfaq`, id]);
  // }

  // Method to delete a FAQ
  onDelete(id: number): void {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.authService.deleteFAQ(id).subscribe({
          next: () => {
            this.faqs = this.faqs.filter(faq => faq.faqId !== id);
            this.noRecordsFound = this.faqs.length === 0;
            Swal.fire('Deleted!', 'Your FAQ has been deleted.', 'success');
          },
          error: (error) => {
            console.error('Error deleting FAQ:', error);
            Swal.fire('Error', 'There was an error deleting the FAQ.', 'error');
          }
        });
      }
    });
  }
}
