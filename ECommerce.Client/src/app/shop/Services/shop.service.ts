import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from 'src/app/shared/Models/Pagination';
import { Product } from 'src/app/shared/Models/Product';
import { ShopParams } from 'src/app/shared/Models/ShopParams';
import { Brand } from 'src/app/shared/Models/brand';
import { Type } from 'src/app/shared/Models/type';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  constructor(private http: HttpClient) {}

  GetListOfProducts(shopparam: ShopParams) {
    let param = new HttpParams();
    if (shopparam.brandId) param = param.append('BrandId', shopparam.brandId);
    if (shopparam.typeId) param = param.append('TypeId', shopparam.typeId);
    if (shopparam.sort) param = param.append('Sort', shopparam.sort);
    if (shopparam.pageIndex)
      param = param.append('pageIndex', shopparam.pageIndex);
    if (shopparam.pageSize)
      param = param.append('pageSize', shopparam.pageSize);
    if (shopparam.Search) param = param.append('Search', shopparam.Search);
    return this.http.get<Pagination<any>>(environment.APIURL + 'product', {
      params: param,
    });
  }

  GetBrands() {
    return this.http.get<any>(environment.APIURL + 'product/ProductBrands');
  }

  GetType() {
    return this.http.get<any>(environment.APIURL + 'product/ProductTypes');
  }

  getSingleProduct(id: number) {
    return this.http.get<any>(environment.APIURL + 'product/' + id);
  }
}
