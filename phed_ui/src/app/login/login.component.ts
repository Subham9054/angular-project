import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import Swal from 'sweetalert2';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
    vchUserName: string = '';
    vchPassWord: string = '';
    vchFullName: string = '';
    role: string = '';
    toggletype: string = 'password';
    showpassword: boolean = false;

    // Variables for random number challenge
    randomNumber1: number = this.generateRandomNumber();
    randomNumber2: number = this.generateRandomNumber();
    userAnswer: number | null = null;
    showError: boolean = false;

    constructor(private authService: AuthService, private router: Router) { }

    ngOnInit() { }
    // Method to toggle password visibility
    enableDisableBtn() {
        this.showpassword = !this.showpassword;
        this.toggletype = this.showpassword ? 'text' : 'password';
    }

    // Method to generate random numbers
    generateRandomNumber(): number {
        return Math.floor(Math.random() * 10);
    }

    // Method to validate the random number challenge
    validateRandomChallenge(): boolean {
        const correctAnswer = this.randomNumber1 + this.randomNumber2;
        if (this.userAnswer !== correctAnswer) {
            this.showError = true;
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Incorrect answer to the challenge. Please try again.',
                heightAuto: false
            });

            // Regenerate the random numbers for the next challenge
            this.randomNumber1 = this.generateRandomNumber();
            this.randomNumber2 = this.generateRandomNumber();
            this.userAnswer = null; // Reset user's answer
            return false;
        }
        this.showError = false;
        return true;
    }

    // Method to handle the login form submission
    Submit() {
        // First check if the user has correctly answered the random number challenge
        if (!this.validateRandomChallenge()) {
            return; // Exit if the challenge is incorrect
        }

        // Check if username and password are provided
        if (!this.vchUserName || !this.vchPassWord) {
            Swal.fire({
                icon: 'warning',
                title: 'Warning',
                text: 'Username and password are required',
                heightAuto: false
            });
            return;
        }

        // Prepare the login payload
        const loginPayload = {
            vchUserName: this.vchUserName,
            vchPassWord: this.vchPassWord,
            vchFullName: this.vchFullName,
            role: this.role
        };

        // Call the AuthService login method
        this.authService.login(loginPayload).subscribe(
            (response) => {
                console.log('Login successful', response);

                // Store token and role in sessionStorage
                sessionStorage.setItem('token', response.token);
                sessionStorage.setItem('role', response.role);
                sessionStorage.setItem('fullName', response.fullName);
                sessionStorage.setItem('cmnmst', response.cmnmst);
                sessionStorage.setItem('cms', response.cms);
                sessionStorage.setItem('config', response.config);
                sessionStorage.setItem('gms', response.gms);
                sessionStorage.setItem('isMisReport', response.isMisReport);
                sessionStorage.setItem('roleid',response.roleid);
                // Set token expiration time (1 minute for demo)
                const expiryTime = new Date().getTime() + (60 * 60 * 1000); // 1 minute
                sessionStorage.setItem('tokenExpiry', expiryTime.toString());

                // Use SweetAlert for successful login
                Swal.fire({
                    icon: 'success',
                    title: 'Login Successful!',
                    heightAuto: false
                }).then(() => {
                    // Clear input fields
                    this.clearFields();

                    // Navigate to the complaint registration page
                    this.router.navigate(['/application/dashboard']);

                });
            },
            (error) => {
                console.error('Login failed', error);

                // Handle validation errors or generic login errors
                if (error.status === 400 && error.error.errors) {
                    const errorMessages = Object.values(error.error.errors).flat().join(', ');
                    Swal.fire({
                        icon: 'error',
                        title: 'Validation Error',
                        text: errorMessages,
                        heightAuto: false
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Login Failed',
                        text: 'Please check your credentials.',
                        heightAuto: false
                    });
                }
            }
        );
    }
    clearFields() {
        this.vchUserName = '';
        this.vchPassWord = '';
        this.vchFullName = '';
        this.role = '';
        this.userAnswer = null;
        this.randomNumber1 = this.generateRandomNumber();
        this.randomNumber2 = this.generateRandomNumber();
    }
}
