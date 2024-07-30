import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../Service/account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  // loginForm!: FormGroup;
  returnUrl: string = "";

  constructor(
    private accountService: AccountService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private totar : ToastrService
  ) {
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
    this.spinner.show();
    this.accountService.login(this.loginForm.value).subscribe({
      next: user => this.router.navigateByUrl(this.returnUrl).finally(
        () => this.SuccessMessage()
      )
    })
    this.spinner.hide();
    
  }


  get Getemail() {
    return this.loginForm.get('email');
  }

  get Getpassword() {
    return this.loginForm.get('password');
  }


  SuccessMessage(){
this.totar.success("Login Successfully" , "Success")
  }

}
