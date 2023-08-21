import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';

import { SharedRoutingModule } from './shared-routing.module';
import { PagerComponent } from './Components/pager/pager.component';
import { PagingHeaderComponent } from './Components/paging-header/paging-header.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { OrderTotalsComponent } from './Components/order-totals/order-totals.component';
import { BasketSummaryComponent } from './Components/basket-summary/basket-summary.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TextInputComponent } from './Components/text-input/text-input.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { NgxSpinnerModule } from 'ngx-spinner';

@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent,
    OrderTotalsComponent,
    BasketSummaryComponent,
    TextInputComponent
  ],
  imports: [
    CommonModule,
    SharedRoutingModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot(),
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    NgxSpinnerModule
  ],
  exports: [
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent,
    CarouselModule,
    OrderTotalsComponent,
    ReactiveFormsModule,
    BsDropdownModule,
    NgxSpinnerModule

  ]
})
export class SharedModule { }
