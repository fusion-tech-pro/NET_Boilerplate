import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RxwebValidators } from "@rxweb/reactive-form-validators"
import { AuthenticationService } from '../authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  showPassword: boolean = false;
  formForRegister: FormGroup;

  constructor(private formBuilder: FormBuilder,
    private authService: AuthenticationService,
    private router: Router) { }

  ngOnInit(): void {
    this.formForRegister = this.formBuilder.group({
      email: [ null, [ Validators.required, Validators.email ]],
      password: [ null, [ Validators.required,
        Validators.pattern(new RegExp('^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]{2,})(?=.*?[#?!@$%^&*-]).{8,}$')) ]],
      confirmPassword: [ null, RxwebValidators.compare( {fieldName:'password'} )]
    });
  }

  register() {
    let value = this.formForRegister.getRawValue();

    this.authService.registerUser(value.email.toLowerCase().trim(), value.password, value.confirmPassword)
    .subscribe(res => {
      localStorage.setItem('encodedJwt', res.encodedJwt);
      localStorage.setItem('expiredDate', res.expiredDate);
      this.router.navigate(['/apps']);  
    })
  }
}