import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatExpansionModule} from '@angular/material/expansion';
import { NgParticlesModule } from 'ng-particles';
import { ProductRoutingModule } from './product-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ProductHeroComponent } from './product-list/product-hero/product-hero.component';
import { NgxSkeletonLoaderModule } from 'ngx-skeleton-loader';
import { ProductItemComponent } from './product-list/product-item/product-item.component';

@NgModule({
  declarations: [ProductListComponent, ProductDetailsComponent, ProductHeroComponent, ProductItemComponent],
  imports: [
    CommonModule,
    ProductRoutingModule,
    SharedModule,
    MatExpansionModule,
    NgParticlesModule,
    NgxSkeletonLoaderModule
  ]
})
export class ProductModule { }
