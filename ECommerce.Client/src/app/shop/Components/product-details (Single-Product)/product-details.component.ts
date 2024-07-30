import { Component, OnInit } from '@angular/core';
import { ShopService } from '../../Services/shop.service';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/Models/Product';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product?: IProduct;
  constructor(private _ShopService: ShopService, private _activeRoute: ActivatedRoute,private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.loadproduct();
  }

  loadproduct() {
    this.spinner.show();
    const id = this._activeRoute.snapshot.paramMap.get('id');
    if (id) {
      this._ShopService.getSingleProduct(+id).subscribe({
        next: (response: any) => 
        { 
          this.product = response.data;
          this.spinner.hide();
        },
        error: (error) => { console.log(error) },
        complete: () => { console.log('load product complete') }
      })
    }
  }

}
