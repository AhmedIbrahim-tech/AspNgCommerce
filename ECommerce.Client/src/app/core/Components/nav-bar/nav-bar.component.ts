import { Component, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/Services/basket.service';
import { BasketItem } from 'src/app/shared/Models/Basket';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  constructor(public basketService : BasketService) { }

  ngOnInit(): void {
  }

  getCount(item: BasketItem[]) {
    if (item == null) {
      return 0;
    }else{
      return item.reduce((sum, item) => sum + item.quantity, 0)
    }
  }
}
