import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IProduct } from '../shared/Models/Product';
import { Pagination } from '../shared/Models/Pagination';
import { BasketService } from '../basket/Services/basket.service';
import { AccountService } from '../account/Service/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'ECommerce Application';
  products: IProduct[] = [];
  constructor(private http: HttpClient, private basketservices: BasketService, private accountService: AccountService) { }

  ngOnInit(): void {
    this.GetListOfProducts();

    const basketId = localStorage.getItem("basket_id");
    if (basketId) this.basketservices.getBasket(basketId);
  }

  GetListOfProducts() {
    this.http.get<Pagination<IProduct[]>>(`${environment.APIURL}/SpecificationsProduct/ListProduct?pageSize=50`).subscribe({
      next: (response: any) => {
        this.products = response.data.data
      },
      error: (error) => { console.log(error); }
    });
  }

  loadCurrentUser() {
    const token = localStorage.getItem('token');
    this.accountService.loadCurrentUser(token).subscribe();
  }
}
