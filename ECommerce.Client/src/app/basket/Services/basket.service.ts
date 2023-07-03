import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Basket } from 'src/app/shared/Models/Basket';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  private basketSourse = new BehaviorSubject<Basket | null>(null);
  basketSourse$ = this.basketSourse.asObservable();

  constructor(private http : HttpClient) { }

  getBasket(id:string){
    return this.http.get<Basket>(environment.APIURL + "/Basket/GetBasket?id" + id).subscribe(
      {
        next: (basket) => {this.basketSourse.next(basket);},
      }
    )
  }

  setBasket(basket : Basket) {
    return this.http.post<Basket>(environment.APIURL + "/Basket/UpdateBasket", basket).subscribe(
      {
        next: (basket) => {this.basketSourse.next(basket);},
      }
    )
  }

  deleteBasket(id:string){
    return this.http.delete<Basket>(environment.APIURL + "/Basket/DeleteBasket?id" + id).subscribe(
      {
        next: (basket) => {this.basketSourse.next(basket);},
      }
    )}

    getcuurentbasketvalue(){
      return this.basketSourse.value;
    }
  }
