import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';

import { SharedRoutingModule } from './shared-routing.module';
import { PagerComponent } from './Components/pager/pager.component';
import { PagingHeaderComponent } from './Components/paging-header/paging-header.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';


@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent
  ],
  imports: [
    CommonModule,
    SharedRoutingModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot()
  ],
  exports: [
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent,
    CarouselModule
  ]
})
export class SharedModule { }
