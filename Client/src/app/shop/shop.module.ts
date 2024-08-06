import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './Components/shop/shop.component';
import { ProductDetailsComponent } from './Components/product-details/product-details.component';
import { ShopsItemComponent } from './Components/shops-item/shops-item.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    ShopComponent,
    ProductDetailsComponent,
    ShopsItemComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports : [ShopComponent]
})
export class ShopModule { }
