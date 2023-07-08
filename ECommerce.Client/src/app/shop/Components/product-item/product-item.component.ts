import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/Models/Product';
import { BasketService } from 'src/app/basket/Services/basket.service';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {
  @Input() product?: IProduct;
  constructor(private _serives:BasketService) { }

  ngOnInit(): void {
  }

addItemToBasket(){
  this.product&& this._serives.addItemToBasket(this.product);
}

}
