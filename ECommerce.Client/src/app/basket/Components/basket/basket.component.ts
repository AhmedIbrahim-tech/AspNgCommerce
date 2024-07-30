import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Basket, BasketItem, BasketTotals } from 'src/app/shared/Models/Basket';
import { BasketService } from '../../Services/basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {

  basket$!: Observable<Basket>;
  basketTotals$!: Observable<BasketTotals>;

  constructor(private basketService: BasketService) { }

  ngOnInit() {
    // this.basket$ = this.basketService.  basketSource$;
    // this.basketTotals$ = this.basketService.basketTotals$;
  }

  removeBasketItem(id:number , quantity:number) {
    this.basketService.removeItemFromBasket(id,quantity);
  }

  incrementItem(item: BasketItem) {
    this.basketService.addItemToBasket(item);
  }

  // decrementItem(item: BasketItem) {
  //   this.basketService.decrementItemQuantity(item);
  // }


}
