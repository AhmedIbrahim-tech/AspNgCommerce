import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/shared/Models/Product';
import { ShopService } from '../../Services/shop.service';
import { retry } from 'rxjs';
import { Brand } from 'src/app/shared/Models/brand';
import { Type } from 'src/app/shared/Models/type';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products: Product[] = [];
  brands: Brand[] = [];
  types: Type[] = [];
  constructor(private _serives: ShopService) { }

  ngOnInit(): void {
    this.GetListOfPro();
    this.GetBrands();
    this.GetTypes();
  }


  GetListOfPro() {
    this._serives.GetListOfProducts().pipe(retry(3)).subscribe({
      next: (response) => { this.products = response.data.data  , console.log(this.products);},
      error: (error) => console.log(error),
      complete: () => { console.log("Data is Completed"); }
    })
  }

  GetBrands() {
    this._serives.GetBrands().pipe(retry(3)).subscribe({
      next: (response) => {this.brands = response.data , console.log("brands " + this.brands)},
      error: (error) => console.log(error),
      complete: () => console.log("Data is Completed")
    })
  }

  GetTypes() {
    this._serives.GetType().pipe(retry(3)).subscribe({
      next: (response) => {this.types = response.data , console.log("types " + this.types)},
      error: (error) => console.log(error),
      complete: () => console.log("Data is Completed")
    })
  }

}
