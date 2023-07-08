import { Component, OnInit } from '@angular/core';
import { ShopService } from '../../Services/shop.service';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/Models/Product';
import { BasketService } from 'src/app/basket/Services/basket.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product?: IProduct;
  quantity = 1;
  quantityInBasket=0;
  constructor(private _ShopService: ShopService, private _activeRoute: ActivatedRoute, private basketService: BasketService) { }

  ngOnInit(): void {
    this.loadproduct();
  }

  loadproduct() {
    const id = this._activeRoute.snapshot.paramMap.get('id');
    if (id) {
      this._ShopService.getSingleProduct(+id).subscribe({
        next: (response: any) => 
        { 
          this.product = response.data;
          this.basketService.basketSource$.pipe(take(1)).subscribe({
            next: basket => {
              const item = basket?.items.find(x=>x.id=== +id);
              if (item) {
                this.quantity = item.quantity;
                this.quantityInBasket = item.quantity;
              }
            }
          })
        },
        error: (error) => { console.log(error) },
        // complete: () => { console.log('load product complete') }
      })
    }
  }

  increamentQuantity(){
    this.quantity++;
  }

  decreamentQuantity(){
    this.quantity--;
  }

  updateBasket(){
    if (this.product) {
      if (this.quantity > this.quantityInBasket) {
        const itemToAdd = this.quantity - this.quantityInBasket;
        this.quantityInBasket += itemToAdd;
        this.basketService.addItemToBasket(this.product , itemToAdd);
      }else{
        const itemToRemove = this.quantityInBasket - this.quantity;
        this.quantityInBasket - +itemToRemove;
        this.basketService.removeItemFromBasket(+this.product.id, itemToRemove);
      }
    }
  }


  get buttonText(){
    return this.quantityInBasket === 0 ? 'Add To Basket' : 'Update Basket'
  }
}
