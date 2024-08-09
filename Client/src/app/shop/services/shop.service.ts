import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ShopParams } from '../../shared/Models/ShopParams';
import { environment } from '../../../environments/environment.development';
import { map } from 'rxjs';
import { IBaseResponse, IPagination } from '../../shared/Models/Paginations';
import { IProduct } from '../../shared/Models/product';
import { ICategorys } from '../../shared/Models/Categorys';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  constructor(private http: HttpClient) { }

  GetListOfProducts(shopparam: ShopParams) {

    let params = new HttpParams();
    if (shopparam.categoryid != 0) {
      params = params.append('Categoryid', shopparam.categoryid.toString());
    }

    params = params.append('Sorting', shopparam.sorting.toString());
    params = params.append('PageNumber', shopparam.pageNumber.toString());
    params = params.append('Pagesize', shopparam.pageSize.toString());
    if (shopparam.search) {
      params = params.append('Search', shopparam.search);
    }
    return this.http.get<IPagination<IProduct>>(environment.baseURL + 'EagerProduct/ListProduct', { observe: 'response', params })
      .pipe(map(response => { return response.body }));
  }

  // GetBrands() {
  //   return this.http.get<IBrand[]>(environment.baseURL + '/SpecificationsProduct/ProductBrands');
  // }

  // GetType() {
  //   return this.http.get<IType[]>(environment.baseURL + '/SpecificationsProduct/ProductTypes');
  // }

  getSingleProduct(id: number) {
    return this.http.get<IBaseResponse<IProduct>>(environment.baseURL + '/EagerProduct/GetByID/' + id);
  }


  getcategorys() {
    return this.http.get<IBaseResponse<ICategorys[]>>(environment.baseURL + 'Category/ListCategory');
  }

}

