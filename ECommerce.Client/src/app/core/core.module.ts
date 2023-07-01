import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CoreRoutingModule } from './core-routing.module';
import { NavBarComponent } from './Components/nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { NotFoundComponent } from './Components/not-found/not-found.component';
import { SectionHeaderComponent } from './Components/section-header/section-header.component';
import { ServerErrorComponent } from './Components/server-error/server-error.component';
import { TestErrorComponent } from './Components/test-error/test-error.component';
import { NgxSpinnerModule } from 'ngx-spinner';
// import { BreadcrumbModule } from 'xng-breadcrumb';


@NgModule({
  declarations: [
    NavBarComponent,
    TestErrorComponent,
    ServerErrorComponent,
    NotFoundComponent,
    SectionHeaderComponent
  ],
  imports: [
    CommonModule,
    CoreRoutingModule,
    RouterModule,
    // BreadcrumbModule,
    ToastrModule.forRoot({
      positionClass:'toast-top-right',
      preventDuplicates: true,
    }),
    NgxSpinnerModule
  ],
  exports: [
    NavBarComponent,
    SectionHeaderComponent,
    NgxSpinnerModule
  ]
})
export class CoreModule { }
