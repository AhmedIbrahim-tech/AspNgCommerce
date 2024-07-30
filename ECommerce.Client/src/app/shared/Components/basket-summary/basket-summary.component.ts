import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BasketItem } from '../../Models/Basket';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.scss']
})
export class BasketSummaryComponent implements OnInit {
  @Output() decrement: EventEmitter<BasketItem> = new EventEmitter<BasketItem>();
  @Output() increment: EventEmitter<BasketItem> = new EventEmitter<BasketItem>();
  @Output() remove: EventEmitter<BasketItem> = new EventEmitter<BasketItem>();
  @Input() isBasket = true;
  @Input() items: BasketItem[] = [];
  @Input() isOrder = false;


  constructor() { }

  ngOnInit(): void {
  }

  decrementItem(item: BasketItem) {
    this.decrement.emit(item);
  }

  incrementItem(item: BasketItem) {
    this.increment.emit(item);
  }

  removeBasketItem(item: BasketItem) {
    this.remove.emit(item);
  }

}
