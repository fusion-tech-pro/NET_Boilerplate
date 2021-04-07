import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '../authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  showPassword: boolean = false;
  formForLogin: FormGroup;

  constructor(private formBuilder: FormBuilder,
    private authService: AuthenticationService,
    private router: Router) { }

  ngOnInit(): void {
    this.formForLogin = this.formBuilder.group({
      email: [ null, [ Validators.required, Validators.email ]],
      password: [ null, [ Validators.required,
        Validators.pattern('^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-])(?=.*?[A-Z]{2,}).{8,}$') ]]
    });
  }

  login() {
    let value = this.formForLogin.getRawValue();

    this.authService.loginUser(value.email.toLowerCase().trim(), value.password)
      .subscribe(res => {
        localStorage.setItem('encodedJwt', res.encodedJwt);
        localStorage.setItem('expiredDate', res.expiredDate);
        this.router.navigate(['/apps']);  
      })
  }
}
