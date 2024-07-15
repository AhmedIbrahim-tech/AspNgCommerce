import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IProduct } from '../shared/Models/Product';
import { Pagination } from '../shared/Models/Pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'ECommerce Application';
  products: IProduct[] = [];
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.GetListOfProducts();
  }

  GetListOfProducts() {
    this.http.get<Pagination<IProduct[]>>(`${environment.APIURL}/SpecificationsProduct/ListProduct?pageSize=50`).subscribe({
      next: (response: any) => {
        this.products = response.data.data
      },
      error: (error) => { console.log(error); }
    });
  }
}
