import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {
  validationErrors: any;

  constructor(private http:HttpClient,private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  get404Error(){
    this.http.get(environment.APIURL + 'Buggy/NotFound').subscribe({
      next: (response) => {console.log(response)},
      error: (error) => {console.log(error)}
    })
  }

  get500Error(){
    this.http.get(environment.APIURL + 'Buggy/ServerError').subscribe({
      next: (response) => {console.log(response)},
      error: (error) => {console.log(error)}
    })
  }

  get400Error(){
    this.http.get(environment.APIURL + 'Buggy/BadRequest').subscribe({
      next: (response) => {console.log(response)},
      error: (error) => {console.log(error)}
    })
  }


  get400ValidationError() {
    this.http.get(environment.APIURL + 'products/ninetynine').subscribe(res => {
      console.log(res);
    }, error => {
      console.log(error);
      this.validationErrors = error.errors;
    });
  }

}
