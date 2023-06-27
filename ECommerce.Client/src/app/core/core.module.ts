import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CoreRoutingModule } from './core-routing.module';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { TestErrorComponent } from './test-error/test-error.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ToastrModule } from 'ngx-toastr';


@NgModule({
  declarations: [
    NavBarComponent,
    TestErrorComponent,
    ServerErrorComponent,
    NotFoundComponent
  ],
  imports: [
    CommonModule,
    CoreRoutingModule,
    RouterModule,
    ToastrModule.forRoot({
      positionClass:'toast-top-right',
      preventDuplicates: true,
    })
  ],
  exports: [
    NavBarComponent
  ]
})
export class CoreModule { }
