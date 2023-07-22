import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../Service/account.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  // loginForm!: FormGroup;
  returnUrl: string = "";

  constructor(private accountService: AccountService, private router: Router, private activatedRoute: ActivatedRoute) {
    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/shop';
  }

  ngOnInit() {
    // this.createLoginForm();
  }

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required]),
    password: new FormControl('', Validators.required)
  });


  /**
   * The onSubmit function calls the login method of the accountService and logs the returned user object
   * to the console.
   */
  onSubmit() {
    this.accountService.login(this.loginForm.value).subscribe({
      next: user => this.router.navigateByUrl(this.returnUrl)
    })
  }


  get Getemail() {
    return this.loginForm.get('email');
  }

  get Getpassword() {
    return this.loginForm.get('password');
  }

}
