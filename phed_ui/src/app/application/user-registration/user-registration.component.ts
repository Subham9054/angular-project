import { Component } from '@angular/core';

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.scss']
})
export class UserRegistrationComponent {
  showpassword: boolean = false;
  toggletype: string = 'password';
    // Method to toggle password visibility
    enableDisableBtn() {
      this.showpassword = !this.showpassword;
      this.toggletype = this.showpassword ? 'text' : 'password';
    }
}
