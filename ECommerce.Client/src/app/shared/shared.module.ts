import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedRoutingModule } from './shared-routing.module';
import { PagingHeaderComponent } from './Components/paging-header/paging-header.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { NgxSpinnerModule } from 'ngx-spinner';


@NgModule({
  declarations: [
    PagingHeaderComponent,
  ],
  imports: [
    CommonModule,
    SharedRoutingModule,
    CarouselModule.forRoot(),
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    NgxSpinnerModule
  ],
  exports: [
    PagingHeaderComponent,
    CarouselModule,
    ReactiveFormsModule,
    BsDropdownModule,
    NgxSpinnerModule
  ]
})
export class SharedModule { }
