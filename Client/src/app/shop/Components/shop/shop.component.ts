import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from '../../../shared/Models/product';
import { ICategorys } from '../../../shared/Models/Categorys';
import { ShopParams } from '../../../shared/Models/ShopParams';
import { ShopService } from '../../services/shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {
  products!: IProduct[];
  categories!: ICategorys[];
  @ViewChild('search') searchterms!:ElementRef;
  ShopParam = new ShopParams;
  sortOptions =
    [
      { name: 'Name', value: 'Name' },
      { name: 'Price : Low to High', value: 'priceAsc' },
      { name: 'Price : high to low', value: 'priceDesc' }
    ]


  constructor(private _shopservice: ShopService){}



  ngOnInit(): void {
    this.getproducts();
    this.getcategoryes();
    
  } 

  getproducts() {
    this._shopservice.GetListOfProducts(this.ShopParam).subscribe((res:any) => {
      console.log("services" , res);
      this.products = res.data
      this.ShopParam.totalCount = res.pageCount;
      this.ShopParam.pageNumber = res.pageNumber;
      this.ShopParam.pageSize = res.pageSize;
    })
  }



  getcategoryes() {
    this._shopservice.getcategorys().subscribe(res => { this.categories = [{ id: 0, name: 'All', description: '' }, ...res.data!] })
  }
  onCategorySelect(categoryid: number) {
    //   this.ShopParams.pageNumber=1;
    this.ShopParam.categoryid = categoryid;

    this.getproducts();
  }
  onSortSelected(sort: Event) {
    let sortValue = (sort.target as HTMLInputElement).value;

    this.ShopParam.sorting = sortValue;

    this.getproducts();
  }
  onPageChanged(event:any)
  {
    if(this.ShopParam.pageNumber !==event){
      this.ShopParam.pageNumber = event;
      this.getproducts(); 
    }
  }
  onSearch(Searchterm:any)
  {
    this.ShopParam.search=Searchterm;
    console.log(Searchterm);
    this.getproducts();
  }
  onSearchInput()
  {

    this.ShopParam.search=this.searchterms.nativeElement.value;
    console.log( this.ShopParam.search);
    this.getproducts();
  }





  
}
