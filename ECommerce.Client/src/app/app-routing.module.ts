import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home/home.component';
import { NotFoundComponent } from './core/Components/not-found/not-found.component';
import { ServerErrorComponent } from './core/Components/server-error/server-error.component';
import { TestErrorComponent } from './core/Components/test-error/test-error.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: 'shop',
    loadChildren: () => import('./shop/shop.module').then(m => m.ShopModule)
  },
  {
    path:'basket',
    loadChildren: () => import('./basket/basket.module').then(m => m.BasketModule)
  },
  // {path:'shop' , component:ShopComponent},
  // {path:'shop/:id' , component:ProductDetailsComponent},
  { path: 'test-error', component: TestErrorComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
