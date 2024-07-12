import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatMenuModule } from '@angular/material/menu';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardLayoutComponent } from './dashboard-layout/dashboard-layout.component';
import { DashboardIndexComponent } from './dashboard-index/dashboard-index.component';
import { SharedModule } from '../shared/shared.module';
import { DashboardSavedItemComponent } from './dashboard-saved-item/dashboard-saved-item.component';
import { DashboardProfileComponent } from './dashboard-profile/dashboard-profile.component';
import { DashboardOrderComponent } from './dashboard-order/dashboard-order.component';

@NgModule({
  declarations: [
    DashboardLayoutComponent,
    DashboardIndexComponent,
    DashboardSavedItemComponent,
    DashboardProfileComponent,
    DashboardOrderComponent
  ],
  imports: [CommonModule, DashboardRoutingModule, SharedModule, MatMenuModule]
})
export class DashboardModule {}
