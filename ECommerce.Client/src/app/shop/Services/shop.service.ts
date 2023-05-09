import { HttpClient, HttpParams } from '@angular/common/http';
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



  GetListOfProducts(BrandId?:number , TypeId?:number){
    let param = new HttpParams();
    if(BrandId) param = param.append("BrandId" , BrandId);
    if(TypeId) param = param.append("TypeId" , TypeId);
    return this.http.get<Pagination<any>>(environment.APIURL + 'product' , {params : param});
  }

  GetBrands(){
    return this.http.get<any>(environment.APIURL + 'product/ProductBrands');
  }

  GetType(){
    return this.http.get<any>(environment.APIURL + 'product/ProductTypes');
  }
}
