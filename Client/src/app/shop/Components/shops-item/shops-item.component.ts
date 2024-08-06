import { Component, Input } from '@angular/core';
import { IProduct } from '../../../shared/Models/product';

@Component({
  selector: 'app-shops-item',
  templateUrl: './shops-item.component.html',
  styleUrl: './shops-item.component.scss'
})
export class ShopsItemComponent {
  @Input() products!:IProduct;
  addItemToBasket()
  {
    //this.basketservices.addItemToBasket(this.products);
  }

}
