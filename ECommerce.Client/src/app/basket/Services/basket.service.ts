import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Basket, BasketItem, BasketTotals } from 'src/app/shared/Models/Basket';
import { IProduct } from 'src/app/shared/Models/Product';
import { DeliveryMethod } from 'src/app/shared/Models/DeliveryMethod';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  private basketSource = new BehaviorSubject<Basket | null>(null);
  basketSource$ = this.basketSource.asObservable();

  private basketTotalSource = new BehaviorSubject<BasketTotals | null>(null);
  basketTotalSource$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) { }

  getBasket(id: string) {
    return this.http.get<Basket>(environment.APIURL + "/Basket/GetBasket?id=" + id).subscribe(
      {
        // next: (basket: Basket) => { this.basketSource.next(basket); }
        next: basket => { 
          this.basketSource.next(basket); 
          this.calculatorTotals();
        }
      }
    )
  }


  setShippingPrice(deliveryMethod:DeliveryMethod)
  {
    let basket = this.getCurrentBasketValue();
    // basket = deliveryMethod.price;
    
    if(basket)
    {
      console.log("shipping price is ",deliveryMethod)
      basket.deliveryMethodId=deliveryMethod.id;
      this.setBasket(basket);
    }
    this.calculatorTotals();
  }

  setBasket(basket: Basket) {
    return this.http.post<Basket>(environment.APIURL + "/Basket/UpdateBasket", basket).subscribe(
      {
        // next: (basket: Basket) => { this.basketSource.next(basket); }
        next: basket => { this.basketSource.next(basket); this.calculatorTotals(); }
      }
    )
  }

 
  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  // addItemToBasket(item: IProduct, quantity = 1) {
  //   const itemToAdd: BasketItem = this.mapProductToBasketItem(item, quantity);
  //   const basket = this.getCurrentBasketValue() ?? this.createBasket();
  //   basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);
  //   this.setBasket(basket);
  // }

  // private addOrUpdateItem(items: BasketItem[], itemToAdd: BasketItem, quantity: number): BasketItem[] {
  //   const index = items.findIndex(i => i.id === itemToAdd.id);
  //   if (index === -1) {
  //     itemToAdd.quantity = quantity;
  //     items.push(itemToAdd);
  //   } else {
  //     items[index].quantity += quantity;
  //   }
  //   return items;
  // }

  // private createBasket(): Basket {
  //   const basket = new Basket();
  //   localStorage.setItem('basket_id', basket.id);
  //   return basket;
  // }

  addItemToBasket(item: IProduct | BasketItem, quantity = 1) {
    if(this.isProduct(item)) item = this.mapProductToBasketItem(item)
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.items = this.addOrUpdateItem(basket.items, item, quantity);
    this.setBasket(basket);
  }
  
  removeItemFromBasket(id : number , quantity = 1) {
    const basket = this.getCurrentBasketValue();
    if(!basket) return;
    const item = basket.items.find(x => x.id === id);
    if(item){
      item.quantity - +quantity;
      basket.items = basket.items.filter(x => x.id !== id)
      if (basket.items.length > 0) {
        this.setBasket(basket);
      } else {
        this.deleteBasket(basket);
      }
    }
  }

  deleteLocalBasket(id: string) {
    this.basketSource.next(null);
    this.basketTotalSource.next(null);
    localStorage.removeItem('basket_id');
  }

    // deleteBasket(id: string) {
  //   return this.http.delete<Basket>(environment.APIURL + "/Basket/DeleteBasket?id" + id).subscribe(
  //     {
  //       next: (basket) => { this.basketSource.next(basket); },
  //     }
  //   )
  // }

  deleteBasket(basket: Basket) {
    return this.http.delete(environment.APIURL + "/Basket/DeleteBasket?id" + basket.id).subscribe({
            next: () => 
            { 
              this.basketSource.next(basket);
              this.basketTotalSource.next(null);
              localStorage.removeItem('basket_id');
            },
          })
  }

  private addOrUpdateItem(items: BasketItem[], itemToAdd: BasketItem, quantity: number): BasketItem[] {
    const item = items.find(x => x.id === itemToAdd.id);
    if (item) {
      item.quantity += quantity;
    } else {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }
    return items;
  }

  private createBasket(): Basket {
    const basket = new Basket();
    localStorage.setItem("basket_id", basket.id);
    return basket;
  }

  private mapProductToBasketItem(item: IProduct) {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      pictureUrl: item.pictureURL,
      quantity : 0,
      brand: item.productBrand,
      type: item.productType
    };
  }

private calculatorTotals(){
  const basket = this.getCurrentBasketValue();
  if (!basket) return;
  const shipping = 0;
  const subtotal = basket.items.reduce((a, b) => (b.price *  b.quantity) + a, 0);
  const total = subtotal + shipping;
  this.basketTotalSource.next({shipping , total , subtotal});
}

private isProduct(item : IProduct | BasketItem) : item is IProduct
{
return (item as IProduct).productBrand !== undefined;
}



}