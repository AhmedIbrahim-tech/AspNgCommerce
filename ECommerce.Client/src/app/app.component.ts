import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Product } from './shared/Models/Product';
import { Pagination } from './shared/Models/Pagination';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'ECommerce Application';
  products: Product[] = [];
  constructor(private http:HttpClient){}

  ngOnInit(): void {
    this.GetListOfProducts();
  }

  GetListOfProducts(){
    this.http.get<any>(`${environment.APIURL}Product?pageSize=50`).subscribe({
      next : response => {
        this.products = response.data.data
        console.log(this.products)
      },
      error : (error) => {console.log(error);},
      complete : () => { console.log("Request Completed"); }
    });
  }
}
