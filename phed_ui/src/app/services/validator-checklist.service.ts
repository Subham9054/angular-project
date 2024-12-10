import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class ValidatorChecklistService {

  constructor() { }

  blankCheck(elmVal: any, msg: any, elmId: any = "") {
    if (elmVal == '' || elmVal == 0 || typeof (elmVal) == undefined || elmVal == null) {
      Swal.fire({
        icon: 'error',
        text: msg
      }).then(function () {
        if (elmId != "") {
          setTimeout(() => {
            const element = <HTMLInputElement>document.getElementById(elmId);
            element.focus();
            element.scrollIntoView({
              behavior: 'smooth', block: 'center'
            });
          }, 500);
        }
      });
      return false;
    }
    return true;
  }

  isSpecialCharKey(elmId: any, elmVal: any, msg: any) {
    let pattern = new RegExp(/[!"#$'()*+,/:;<=>[\]^`{|}~]/, 'g');
    if (elmVal != '') {
      if (pattern.test(elmVal) == true) {
        Swal.fire({
          icon: 'error',
          text: 'Special character not allowed in ' + msg
        }).then(function () {
          if (elmId != "") {
            setTimeout(() => {
              const element = <HTMLInputElement>document.getElementById(elmId);
              element.focus();
              element.scrollIntoView({ behavior: 'smooth', block: 'center' });
            }, 500);
          }
        });
        return false;
      }
      else {
        return true;
      }
    }
    return true;
  }

  isCharecterKey(elmId: any, elmVal: any, msg: any) {
    if (elmVal != '') {
      const val = /^[A-Za-z ]+$/;
      if (val.test(elmVal)) {
        return true;
      }
      else {
        Swal.fire({
          icon: 'error',
          text: msg + ' only character allowed!'
        }).then(function () {
          if (elmId != "") {
            setTimeout(() => {
              const element = <HTMLInputElement>document.getElementById(elmId);
              element.focus();
              element.scrollIntoView({ behavior: 'smooth', block: 'center' });
            }, 500);
          }
        });
        return false;
      }
    }
    return true;
  }

  selectDropdown(elmVal: any, msg: any) {
    if (elmVal == 0 || elmVal == '' || typeof (elmVal) == undefined || elmVal == null) {
      Swal.fire({
        icon: 'error',
        text: 'Select ' + ' ' + msg
      });
      return false;
    }
    return true;
  }

  maxLength(elmId: any, elmVal: any, fldLngth: any, msg: any) {
    if (elmVal.length > 0 && elmVal.length > fldLngth) {
      Swal.fire({
        icon: 'error',
        text: msg + ' should not more than ' + fldLngth + ' characters'
      }).then(function () {
        if (elmId != "") {
          setTimeout(() => {
            const element = <HTMLInputElement>document.getElementById(elmId);
            element.focus();
            element.scrollIntoView({ behavior: 'smooth', block: 'center' });
          }, 500);
        }
      });
      return false;
    }
    return true;
  }

  minLength(elmVal: any, fldLngth: any, msg: any, elmId: any = "") {
    if (elmVal.length > 0 && elmVal.length < fldLngth) {
      Swal.fire({
        icon: 'error',
        text: msg + ' should not be less than ' + fldLngth + ' characters'
      }).then(function () {
        if (elmId != "") {
          setTimeout(() => {
            const element = <HTMLInputElement>document.getElementById(elmId);
            element.focus();
            element.scrollIntoView({ behavior: 'smooth', block: 'center' });
          }, 500);
        }
      });
      return false;
    }
    return true;
  }


  validEmail(elmVal: any, elmId: any = "", msg: any = "") {
    // Updated regular expression to ensure the email ends with .com
    if (/^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z]{2,4})+$/.test(elmVal)) {
      return (true)
    }
    Swal.fire({
      icon: 'error',
      text: 'Enter a valid ' + msg
    }).then(function () {
      if (elmId != "") {
        setTimeout(() => {
          const element = <HTMLInputElement>document.getElementById(elmId);
          element.focus();
          element.scrollIntoView({ behavior: 'smooth', block: 'center' });
        }, 500);
      }
    });
    return (false)
  }


  validMob(elmVal: any, elmId: any = "", msg: any = "") {
    let pattern = new RegExp(/^[6-9][0-9]{9}$/);
    if (elmVal != '') {
      if (pattern.test(elmVal) == true) {
        return true;
      } else {
        if (elmId == "alternatemobileno") {
          Swal.fire({
            icon: 'error',
            text: 'Please enter a valid alternate mobile no.'
          }).then(function () {
            if (elmId != "") {
              setTimeout(() => {
                const element = <HTMLInputElement>document.getElementById(elmId);
                element.focus();
                element.scrollIntoView({ behavior: 'smooth', block: 'center' });
              }, 500);
            }
          });
          return false;
        }
        else
          Swal.fire({
            icon: 'error',
            text: 'Please enter a valid mobile no.'
          }).then(function () {
            if (elmId != "") {
              setTimeout(() => {
                const element = <HTMLInputElement>document.getElementById(elmId);
                element.focus();
                element.scrollIntoView({ behavior: 'smooth', block: 'center' });
              }, 500);
            }
          });
        return false;
      }
    }
    return true;
  }
}
