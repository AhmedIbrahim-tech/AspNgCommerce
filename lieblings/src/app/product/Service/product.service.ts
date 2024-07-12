import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from 'src/app/shared/Models/Pagination';
import { IProduct } from 'src/app/shared/Models/Product';
import { ShopParams } from 'src/app/shared/Models/ShopParams';
import { IBrand } from 'src/app/shared/Models/brand';
import { IType } from 'src/app/shared/Models/type';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

  GetListOfProducts(shopparam: ShopParams) {

    let param = new HttpParams();
    if (shopparam.brandId > 0)  param = param.append('BrandId', shopparam.brandId);
    if (shopparam.typeId > 0)   param = param.append('TypeId', shopparam.typeId);
    if (shopparam.sort)         param = param.append('Sort', shopparam.sort);
    if (shopparam.pageNumber)    param = param.append('pageIndex', shopparam.pageNumber);
    if (shopparam.pageSize)     param = param.append('pageSize', shopparam.pageSize);
    if (shopparam.Search)       param = param.append('Search', shopparam.Search);

    return this.http.get<Pagination<IProduct[]>>(environment.APIURL + '/SpecificationsProduct/ListProduct', {
      params: param,
    });
  }

  GetBrands() {
    return this.http.get<IBrand[]>(environment.APIURL + '/SpecificationsProduct/ProductBrands');
  }

  GetType() {
    return this.http.get<IType[]>(environment.APIURL + '/SpecificationsProduct/ProductTypes');
  }

  getSingleProduct(id: number) {
    return this.http.get<IProduct>(environment.APIURL + '/SpecificationsProduct/GetByID/' + id);
  }
}
