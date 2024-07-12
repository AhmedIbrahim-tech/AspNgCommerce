import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DocComponentsComponent } from './doc-components/doc-components.component';
import { DocCreditsComponent } from './doc-credits/doc-credits.component';
import { DocCustomizationsComponent } from './doc-customizations/doc-customizations.component';
import { DocFolderStructureComponent } from './doc-folder-structure/doc-folder-structure.component';
import { DocIndexComponent } from './doc-index/doc-index.component';
import { DocLayoutComponent } from './doc-layout/doc-layout.component';

const childRoutes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: DocIndexComponent
  },
  {
    path: 'folder-structure',
    component: DocFolderStructureComponent
  },
  {
    path: 'components',
    component: DocComponentsComponent
  },
  {
    path: 'customizations',
    component: DocCustomizationsComponent
  },
  {
    path: 'credits',
    component: DocCreditsComponent
  }
];
const routes: Routes = [
  {
    path: '',
    component: DocLayoutComponent,
    children: childRoutes
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DocRoutingModule {}
