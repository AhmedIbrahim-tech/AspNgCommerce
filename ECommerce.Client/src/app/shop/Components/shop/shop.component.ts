import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/Models/Product';
import { ShopService } from '../../Services/shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products : IProduct[] = [];
  constructor(private _serives:ShopService) { }

  ngOnInit(): void {
    this.GetListOfProducts();
  }


  GetListOfProducts(){
    this._serives.GetListOfProducts().subscribe({
        next : response => {this.products = response.data},
        error : (error) => { console.log(error);},
        complete : () => {console.log("Data is Completed");}
      });
  }

}
