import { Component, OnInit, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-forgotpassword',
  templateUrl: './forgotpassword.component.html',
  styleUrls: ['./forgotpassword.component.scss']
})
export class ForgotpasswordComponent implements OnInit, OnDestroy {

  time: string = '03:00';  // Start time
  private timer: any;

  constructor() {}




  otpVisible: boolean = false;
  userIdVisible: boolean = false;
  times: number = 2; // Time for OTP countdown, example: 2 minutes
  mobileNumber: string = '';

  getOtp() {
    // Logic for OTP request
    this.otpVisible = true;
    this.userIdVisible = false; // Ensure the User ID section is hidden
  }

  onSubmit() {
    // Logic for submitting OTP (if correct) or moving to the next step
    this.otpVisible = false; // Hide OTP part
    this.userIdVisible = true; // Show the User ID section after OTP is verified
  }

  ngOnInit(): void {
    this.startCountdown();
  }

  ngOnDestroy(): void {
    if (this.timer) {
      clearInterval(this.timer);  // Clean up when component is destroyed
    }
  }

  // Function to start the countdown
  startCountdown() {
    let [minutes, seconds] = this.time.split(':').map(Number);

    this.timer = setInterval(() => {
      if (seconds === 0 && minutes === 0) {
        clearInterval(this.timer);  // Stop when timer reaches 00:00
      } else {
        if (seconds === 0) {
          seconds = 59;
          minutes--;
        } else {
          seconds--;
        }
        this.time = `${this.padZero(minutes)}:${this.padZero(seconds)}`;
      }
    }, 1000);
  }

  // Function to add leading zero to single-digit minutes and seconds
  padZero(num: number): string {
    return num < 10 ? `0${num}` : `${num}`;
  }




}
