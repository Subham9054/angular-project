import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-mngcontact',
  templateUrl: './mngcontact.component.html',
  styleUrls: ['./mngcontact.component.scss']
})
export class MngcontactComponent implements OnInit {
  contactModel: any = {
    contactId: null,
    addressEnglish: '',
    addressHindi: '',
    mobileEnglish: '',
    mobileHindi: '',
    email: '',
    embeddedUrl: '',
    isEditing: false,
  };

  constructor(private authService: AuthService, private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.checkForEdit();
  }

  // Check if we are in edit mode by looking for the ID in the route parameters
  checkForEdit(): void {
    this.route.params.subscribe((params) => {
      const id = +params['id']; // Convert the route parameter to a number
      if (id) {
        this.contactModel.isEditing = true;
        this.loadContactData(id);
      } else {
        this.contactModel.isEditing = false;
      }
    });
  }

  loadContactData(contactId: number): void {
    debugger;
    this.authService.getContactById(contactId).subscribe({
      next: (response: any) => {
        // Check if the response is successful and contains valid data
        if (response?.success && response.data) {
          // Ensure response.data is an object, not an array
          const data = Array.isArray(response.data) ? response.data[0] : response.data;
  
          // Map the fetched data to the eventModel
          this.contactModel = {
            contactId: data?.contactId || null,
            addressEnglish: data?.addressEnglish || '',
            addressHindi: data?.addressHindi || '',
            mobileEnglish: data?.mobileEnglish || '',
            mobileHindi: data?.mobileHindi || '',
            email: data?.email || '',
            embeddedUrl: data?.embeddedUrl || '',
            isEditing: true, // Ensure editing mode is enabled
          };
        } else {
          console.error('Error fetching contact details:', response?.message || 'Invalid response structure.');
          Swal.fire('Error', response?.message || 'Failed to load contact details.', 'error');
        }
      },
      error: (error) => {
        console.error('Error fetching contact details:', error);
        Swal.fire('Error', 'Failed to load contact details.', 'error');
      },
    });
  }

  submitForm(): void {
    debugger;
    // Validation: Address (English)
    if (!this.contactModel.addressEnglish || this.contactModel.addressEnglish.trim() === '') {
      Swal.fire('Validation Error', 'Please enter the Address in English.', 'error');
      return;
    }

    // Validation: Address (Hindi)
    if (!this.contactModel.addressHindi || this.contactModel.addressHindi.trim() === '') {
      Swal.fire('Validation Error', 'कृपया हिंदी में पता दर्ज करें।', 'error');
      return;
    }

    // Validation: Mobile (English)
    if (!this.contactModel.mobileEnglish || this.contactModel.mobileEnglish.trim() === '' || this.contactModel.mobileEnglish.length !== 10) {
      Swal.fire('Validation Error', 'Please enter a valid 10-digit Mobile Number in English.', 'error');
      return;
    }

    // Validation: Mobile (Hindi)
    if (!this.contactModel.mobileHindi || this.contactModel.mobileHindi.trim() === '' || this.contactModel.mobileHindi.length !== 10) {
      Swal.fire('Validation Error', 'कृपया हिंदी में 10 अंकों का सही मोबाइल नंबर दर्ज करें।', 'error');
      return;
    }

    // Validation: Email
    if (!this.contactModel.email || this.contactModel.email.trim() === '' || !this.validateEmail(this.contactModel.email)) {
      Swal.fire('Validation Error', 'Please enter a valid Email address.', 'error');
      return;
    }

    // All validations passed; proceed with form submission
    const formData = new FormData();
    formData.append('addressEnglish', this.contactModel.addressEnglish);
    formData.append('addressHindi', this.contactModel.addressHindi);
    formData.append('mobileEnglish', this.contactModel.mobileEnglish);
    formData.append('mobileHindi', this.contactModel.mobileHindi);
    formData.append('email', this.contactModel.email);
    formData.append('embeddedUrl', this.contactModel.embeddedUrl);

    if (this.contactModel.contactId) {
      formData.append('contactId', this.contactModel.contactId.toString());
    }

    // Submit the form using the authService
    this.authService.createOrUpdateContact(formData, this.contactModel.isEditing ? this.contactModel.contactId : undefined).subscribe({
      next: (response: any) => {
        const message = this.contactModel.isEditing
          ? 'Contact details updated successfully!'
          : 'Contact details created successfully!';
        Swal.fire('Success', message, 'success');
        this.router.navigate(['/application/mngcontact']);
        this.resetForm();
      },
      error: (error) => {
        console.error('Error:', error);
        Swal.fire('Error', `An error occurred while processing the contact.`, 'error');
      },
    });
  }

  // Utility method to validate email format 
  private validateEmail(email: string): boolean {
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailPattern.test(email);
  }

  resetForm(): void {
    this.contactModel = {
      contactId: null,
      addressEnglish: '',
      addressHindi: '',
      mobileEnglish: '',
      mobileHindi: '',
      email: '',
      embeddedUrl: '',
      isEditing: false      
    };
  }
}
