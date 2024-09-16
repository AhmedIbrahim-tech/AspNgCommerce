import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ContactRoutingModule } from './contact-routing.module';
import { ContactComponent } from './contact.component';
import { SharedModule } from '../shared/shared.module';
import { GoogleLocationMapComponent } from './google-location-map/google-location-map.component';
import { GoogleMapsModule } from '@angular/google-maps';
import { provideHttpClient, withJsonpSupport } from '@angular/common/http';

@NgModule({ declarations: [ContactComponent, GoogleLocationMapComponent],
    exports: [GoogleLocationMapComponent], imports: [CommonModule, ContactRoutingModule, SharedModule, GoogleMapsModule], providers: [provideHttpClient(withJsonpSupport())] })
export class ContactModule {}
