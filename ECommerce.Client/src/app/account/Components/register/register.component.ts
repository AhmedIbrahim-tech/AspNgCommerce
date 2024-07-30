import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../../Service/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private fb: FormBuilder, private accountservice: AccountService, private router: Router) { }
  
  
/* The code `registerForm = this.fb.group({ ... })` is creating an instance of a form group using the
`FormBuilder` service. */
  registerForm = this.fb.group({
    displayName: ['', Validators.required],
    email: ['', Validators.required],
    password: ['', Validators.required],
  })

  onSubmit() {
    this.accountservice.register(this.registerForm.value).subscribe({
      next:user => this.router.navigateByUrl("/shop")
    })
  }

  
  get Getemail() {
    return this.registerForm.get('email');
  }

  get GetdisplayName(){
    return this.registerForm.get("displayName");
  }

  get Getpassword() {
    return this.registerForm.get('password');
  }

  ngOnInit(): void {
  }

}
