import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShopRoutingModule } from './shop-routing.module';
import { ShopComponent } from './Components/shop/shop.component';
import { AppRoutingModule } from '../app-routing.module';
import { ProductItemComponent } from './Components/product-item/product-item.component';


@NgModule({
  declarations: [
    ShopComponent,
    ProductItemComponent
  ],
  imports: [
    CommonModule,
    ShopRoutingModule,
    AppRoutingModule
  ],
  exports : [
    ShopComponent
  ]
})
export class ShopModule { }
