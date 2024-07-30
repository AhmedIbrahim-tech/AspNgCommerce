import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CoreRoutingModule } from './core-routing.module';
import { NavBarComponent } from './Components/nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { SharedModule } from '../shared/shared.module';
import { ContactComponent } from './Components/contact/contact.component';


@NgModule({
  declarations: [
    NavBarComponent,
    ContactComponent
  ],
  imports: [
    CommonModule,
    CoreRoutingModule,
    RouterModule,
    ToastrModule.forRoot({
      positionClass:'toast-top-right',
      preventDuplicates: true,
      timeOut: 2000,
    }),
    NgxSpinnerModule,
    SharedModule
    
  ],
  exports: [
    NavBarComponent,
    NgxSpinnerModule,
  ]
})
export class CoreModule { }
