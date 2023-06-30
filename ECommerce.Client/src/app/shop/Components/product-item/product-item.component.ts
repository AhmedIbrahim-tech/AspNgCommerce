import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/Models/Product';
import { ShopService } from '../../Services/shop.service';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {
  @Input() product?: IProduct;
  constructor(private _serives:ShopService) { }

  ngOnInit(): void {
  }



}
