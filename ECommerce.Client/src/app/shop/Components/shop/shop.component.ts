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
  //#region Variable

  products: Product[] = [];
  brands: Brand[] = [];
  types: Type[] = [];

  SelectTypeId: number = 0;
  SelectBrandId: number = 0;

  //#endregion


  constructor(private _serives: ShopService) { }

  ngOnInit(): void {
    this.GetListOfProducts();
    this.GetBrands();
    this.GetTypes();
  }


  GetListOfProducts() {
    this._serives.GetListOfProducts(this.SelectBrandId , this.SelectTypeId).pipe(retry(3)).subscribe({
      next: (response) => { this.products = response.data.data },
      error: (error) => console.log(error),
      complete: () => { console.log("Data is Completed"); }
    })
  }

  GetBrands() {
    this._serives.GetBrands().pipe(retry(3)).subscribe({
      next: response => this.brands = [{ id: 0, name: "All" }, ...response.data],
      error: (error) => console.log(error),
      complete: () => console.log("Data is Completed")
    })
  }

  GetTypes() {
    this._serives.GetType().pipe(retry(3)).subscribe({
      next: (response) => { this.types = [{ id: 0, name: "All" }, ...response.data] },
      error: (error) => console.log(error),
      complete: () => console.log("Data is Completed")
    })
  }


  onBrandSelected(brandId: number) {
    this.SelectBrandId = brandId;
    this.GetListOfProducts();
  }

  onTypeSelected(typeId: number) {
    this.SelectTypeId = typeId;
    this.GetListOfProducts();
  }

}
