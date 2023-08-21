import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from 'src/app/shared/Models/Product';
import { ShopService } from '../../Services/shop.service';
import { retry } from 'rxjs';
import { IBrand } from 'src/app/shared/Models/brand';
import { IType } from 'src/app/shared/Models/type';
import { ShopParams } from 'src/app/shared/Models/ShopParams';
import { NgxSpinnerService } from "ngx-spinner";
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  //#region Variable

  products: IProduct[] = [];
  brands: IBrand[] = [];
  types: IType[] = [];

  shopparam = new ShopParams();
  TotalCount = 0;
  @ViewChild("search") searchTerms?: ElementRef;

  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' },
  ];

  //#endregion


  constructor(private _serives: ShopService, private spinner: NgxSpinnerService,private toastr: ToastrService) { }

  ngOnInit(): void {
    this.GetListOfProducts();
    this.GetBrands();
    this.GetTypes();
  }

  //#region retrieves a list

  /**
   * The function `GetListOfProducts()` retrieves a list of products from a service, handles the
   * response, and updates the necessary variables.
   */
  GetListOfProducts() {
    this.spinner.show();
    this._serives.GetListOfProducts(this.shopparam).pipe(retry(3)).subscribe({
      next: (response: any) => {
        this.products = response.data.data;
        this.shopparam.pageNumber = response.data.pageIndex;
        this.shopparam.pageSize = response.data.pageSize;
        this.TotalCount = response.data.count;
        this.spinner.hide();
        this.showSuccess()
        
      },
      error: (error) => console.log(error),
      // complete: () => { console.log("Data is Completed"); }
    })
  }

  /**
   * The function `GetBrands()` retrieves a list of brands from a service, adds an "All" option to the
   * beginning of the list, and assigns the result to the `brands` variable.
   */
  GetBrands() {
    this._serives.GetBrands().pipe(retry(3)).subscribe({
      next: (response: any) => this.brands = [{ id: 0, name: "All" }, ...response.data],
      error: (error) => console.log(error),
      // complete: () => console.log("Data is Completed")
    })
  }

  /**
   * The function `GetTypes()` retrieves types from a service and assigns them to a variable called
   * `types`, including an additional "All" type.
   */
  GetTypes() {
    this._serives.GetType().pipe(retry(3)).subscribe({
      next: (response: any) => { this.types = [{ id: 0, name: "All" }, ...response.data] },
      error: (error) => console.log(error),
      // complete: () => console.log("Data is Completed")
    })
  }
  //#endregion

  //#region Selected

  /**
   * The function `onBrandSelected` updates the `brandId` property of `shopparam`, sets the
   * `pageNumber` property to 1, and then calls the `GetListOfProducts` function.
   * @param {number} brandId - The brandId parameter is a number that represents the selected brand.
   */
  onBrandSelected(brandId: number) {
    this.shopparam.brandId = brandId;
    this.shopparam.pageNumber = 1;
    this.GetListOfProducts();
  }

  /**
   * The function updates the typeId property of the shopparam object, sets the pageNumber property to
   * 1, and calls the GetListOfProducts function.
   * @param {number} typeId - The typeId parameter is a number that represents the selected type of
   * product.
   */
  onTypeSelected(typeId: number) {
    this.shopparam.typeId = typeId;
    this.shopparam.pageNumber = 1;
    this.GetListOfProducts();
  }

  /**
   * The function updates the sort parameter and retrieves a list of products.
   * @param {any} event - The event parameter is an object that represents the event that triggered the
   * function. It contains information about the event, such as the target element that triggered the
   * event.
   */
  OnSortSelected(event: any) {
    this.shopparam.sort = event.target.value;
    this.GetListOfProducts();
  }

  //#endregion

  //#region pagination 

  /**
   * The onPageChanged function updates the page number in the shopparam object and calls the
   * GetListOfProducts function if the page number has changed.
   * @param {any} event - The event parameter is an object that contains information about the page
   * change event. It typically includes properties such as the new page number and any other relevant
   * data related to the page change.
   */
  onPageChanged(event: any) {
    if (this.shopparam.pageNumber !== event.page) {
      this.shopparam.pageNumber = event.page;
      this.GetListOfProducts();
    }
  }

  //#endregion

  //#region Events

  /**
   * The `OnSearch()` function updates the search parameter, resets the page number, and retrieves a
   * list of products based on the search term.
   */
  OnSearch() {
    this.shopparam.Search = this.searchTerms?.nativeElement.value;
    this.shopparam.pageNumber = 1;
    this.GetListOfProducts();
  }

  /**
   * The function "OnRest" clears the search terms, resets the shop parameters, and retrieves a list of
   * products.
   */
  OnRest() {
    if (this.searchTerms) this.searchTerms.nativeElement.value = "";
    this.shopparam = new ShopParams();
    this.GetListOfProducts();
  }
  //#endregion


  showSuccess() {
    this.toastr.success('Data Loading Successfully', 'Success');
  }

}