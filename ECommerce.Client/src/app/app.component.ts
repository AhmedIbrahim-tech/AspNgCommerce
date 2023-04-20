import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IProduct } from './shared/Models/Product';
import { Pagination } from './shared/Models/Pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'ECommerce Application';
  products : IProduct[] = [];
  constructor(private http:HttpClient){}

  ngOnInit(): void {
    this.http.get<Pagination<IProduct[]>>("https://localhost:44332/api/Product?PageSize=50").subscribe({
      next : (response) => {console.log(response); this.products = response.data },
      error : (error) => {console.log(error);},
      complete : () => { console.log("Request Completed"); }
    });
  }

}
