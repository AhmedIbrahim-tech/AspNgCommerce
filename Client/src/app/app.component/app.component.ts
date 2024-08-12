import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { error } from 'console';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
/* The `AppComponent` class in TypeScript defines a component with a `title` property and a method
`getproduct()` that makes an HTTP GET request to retrieve product data. */
export class AppComponent implements OnInit {
  title = 'Client';

  constructor(private _httpclient : HttpClient){
  }


  ngOnInit(): void {
    this.getproduct();
  }


  getproduct(){
    this._httpclient.get('https://localhost:44318/api/v1/EagerProduct/ListProduct').subscribe(
      (data:any) => {
        console.log(data);
      },
      error => {
        console.log(error);
      }
    );
  }

}
