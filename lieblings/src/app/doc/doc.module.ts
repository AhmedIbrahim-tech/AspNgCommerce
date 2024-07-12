import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatListModule } from '@angular/material/list';
import { MatTreeModule } from '@angular/material/tree';
import { DocRoutingModule } from './doc-routing.module';
import { SharedModule } from '../shared/shared.module';
import { DocLayoutComponent } from './doc-layout/doc-layout.component';
import { DocIndexComponent } from './doc-index/doc-index.component';
import { DocCreditsComponent } from './doc-credits/doc-credits.component';
import { DocFolderStructureComponent } from './doc-folder-structure/doc-folder-structure.component';
import { DocCustomizationsComponent } from './doc-customizations/doc-customizations.component';
import { DocComponentsComponent } from './doc-components/doc-components.component';

@NgModule({
  declarations: [
    DocLayoutComponent,
    DocIndexComponent,
    DocCreditsComponent,
    DocFolderStructureComponent,
    DocCustomizationsComponent,
    DocComponentsComponent
  ],
  imports: [CommonModule, DocRoutingModule, SharedModule, MatListModule, MatTreeModule]
})
export class DocModule {}
