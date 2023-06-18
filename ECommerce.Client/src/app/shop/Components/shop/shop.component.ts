import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Product } from 'src/app/shared/Models/Product';
import { ShopService } from '../../Services/shop.service';
import { retry } from 'rxjs';
import { Brand } from 'src/app/shared/Models/brand';
import { Type } from 'src/app/shared/Models/type';
import { ShopParams } from 'src/app/shared/Models/ShopParams';

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

  shopparam = new ShopParams();
  TotalCount = 0;
  @ViewChild("search") searchTerms!: ElementRef;

  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' },
  ];

  //#endregion


  constructor(private _serives: ShopService) { }

  ngOnInit(): void {
    this.GetListOfProducts();
    this.GetBrands();
    this.GetTypes();
  }


  GetListOfProducts() {
    this._serives.GetListOfProducts(this.shopparam).pipe(retry(3)).subscribe({
      next: (response) => 
      { 
        this.products = response.data.data; 
        this.shopparam.pageIndex = response.data.pageIndex;
        this.shopparam.pageSize = response.data.pageSize;
        this.TotalCount = response.data.count;
      },
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
    this.shopparam.brandId = brandId;
    this.shopparam.brandId = 1;
    this.GetListOfProducts();
  }

  onTypeSelected(typeId: number) {
    this.shopparam.typeId = typeId;
    this.shopparam.typeId = 1;
    this.GetListOfProducts();
  }

  OnSortSelected(event: any) {
    this.shopparam.sort = event.target.value;
    this.GetListOfProducts();
  }

  onPageChanged(event:any){
    if (this.shopparam.pageIndex !== event.page) {
      this.shopparam.pageIndex = event.page;
      this.GetListOfProducts();
    }
  }

  OnSearch(){
    this.shopparam.Search = this.searchTerms.nativeElement.value;
    this.shopparam.pageIndex = 1;
    this.GetListOfProducts();
  }

  OnRest(){
    if(this.searchTerms) this.searchTerms.nativeElement.value = "";
    this.shopparam = new ShopParams();
    this.GetListOfProducts();
  }
}
