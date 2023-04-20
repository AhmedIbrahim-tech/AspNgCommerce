import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from 'src/app/shared/Models/Pagination';
import { IProduct } from 'src/app/shared/Models/Product';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  constructor(private http:HttpClient) { }



  GetListOfProducts(){
    return this.http.get<Pagination<IProduct[]>>('https://localhost:44332/api/Product');
  }
}
