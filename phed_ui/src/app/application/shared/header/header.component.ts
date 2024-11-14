import { Component, ElementRef, HostListener } from '@angular/core';
import { Router } from '@angular/router'; // Import Router for navigation
import { AuthService } from 'src/app/auth.service'; // Adjust the import path as needed
import Swal from 'sweetalert2'; // Import SweetAlert2

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  toggleLayoutBtn: boolean = false;

  constructor(private eRef: ElementRef, private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
    // Toggle layout
    if (localStorage.getItem('layoutType') === '') {
      this.setLayout('');
      this.toggleLayoutBtn = false;
    } else {
      this.setLayout('');
      this.toggleLayoutBtn = false;
    }
  }

  @HostListener('document:click', ['$event'])
  clickout(event: any) {
    if (this.eRef.nativeElement.contains(event.target)) {
      if (localStorage.getItem('layoutType') === '') {
        this.setLayout('toggle-layout');
        this.toggleLayoutBtn = true;
      } else {
        this.setLayout('');
        this.toggleLayoutBtn = false;
      }
    } else {
      if (localStorage.getItem('layoutType') === 'toggle-layout') {
        this.setLayout('');
        this.toggleLayoutBtn = false;
      }
    }
  }

  setLayout(layoutToggle: any) {
    localStorage.setItem('layoutType', layoutToggle);
    document.body.className = layoutToggle;
  }

  logout() {
    Swal.fire({
        title: 'Logged Out!',
        text: 'You have been logged out successfully.',
        icon: 'success',
        timer: 1000, // Set a timer to auto-close the alert after 2 seconds
        showConfirmButton: false, // Remove the confirm button
        willClose: () => {
            // Clear session storage and perform logout
            this.authService.logout(); // Call the logout function from the auth service
            sessionStorage.removeItem('token');
            sessionStorage.removeItem('tokenExpiry');
            
            // Navigate to the login page after the message is displayed
            this.router.navigate(['/login']);
        }
    });
  }

  // Language change
  buttonText: string = 'हिंदी';

  toggleText() {
    this.buttonText = this.buttonText === 'हिंदी' ? 'EN' : 'हिंदी';
  }
}
