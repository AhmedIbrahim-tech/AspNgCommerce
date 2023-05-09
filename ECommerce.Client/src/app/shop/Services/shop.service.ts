import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from 'src/app/shared/Models/Pagination';
import { Product } from 'src/app/shared/Models/Product';
import { Brand } from 'src/app/shared/Models/brand';
import { Type } from 'src/app/shared/Models/type';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  constructor(private http:HttpClient) { }



  GetListOfProducts(){
    return this.http.get<any>(environment.APIURL + 'product');
  }

  GetBrands(){
    return this.http.get<any>(environment.APIURL + 'product/ProductBrands');
  }

  GetType(){
    return this.http.get<any>(environment.APIURL + 'product/ProductTypes');
  }
}
