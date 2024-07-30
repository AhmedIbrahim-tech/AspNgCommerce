import { Component, Input, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/Services/basket.service';

@Component({
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.scss']
})
export class OrderTotalsComponent implements OnInit {
  @Input() shippingPrice?: number;
  @Input() subtotal?: number;
  @Input() total?: number;

  constructor(public basketService : BasketService) { }

  ngOnInit(): void {
  }

}
