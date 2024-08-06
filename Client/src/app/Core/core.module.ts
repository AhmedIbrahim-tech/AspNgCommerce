import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VarBarComponent } from './var-bar/var-bar.component';
import { FooterBarComponent } from './footer-bar/footer-bar.component';
import { RouterModule } from '@angular/router';
import { TestErrorComponent } from './test-error/test-error.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { SectionHeaderComponent } from './section-header/section-header.component';
import { DemoPageComponent } from './demo-page/demo-page.component';




@NgModule({
  declarations: [
   
    VarBarComponent,
    FooterBarComponent,
    TestErrorComponent,
    NotFoundComponent,
    ServerErrorComponent,
    SectionHeaderComponent,
    DemoPageComponent
 
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports:
  [    
    VarBarComponent,
    FooterBarComponent,
    SectionHeaderComponent,
    DemoPageComponent
  ]
})
export class CoreModule { }
