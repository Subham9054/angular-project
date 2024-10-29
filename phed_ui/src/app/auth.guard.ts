import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import Swal from 'sweetalert2'; // Import SweetAlert

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router) {}

  canActivate(): boolean {
    const token = sessionStorage.getItem('token');
    const tokenExpiry = sessionStorage.getItem('tokenExpiry');

    if (token && tokenExpiry) {
      const now = new Date().getTime();
      const expiryTime = Number(tokenExpiry);

      // Check if the token is expired
      if (now > expiryTime) {
        sessionStorage.clear(); // Clear all session data
        
        // Show SweetAlert to notify user that the session has expired
        Swal.fire({
          icon: 'warning',
          title: 'Session Expired',
          text: 'Your session has expired. Please log in again.',
          confirmButtonText: 'OK'
        }).then(() => {
          // Redirect to login after user clicks 'OK'
          this.router.navigate(['/login']);
        });

        return false;
      }

      return true; // Token is valid
    }

    // No token found, redirect to login
    this.router.navigate(['/login']);
    return false;
  }
}
