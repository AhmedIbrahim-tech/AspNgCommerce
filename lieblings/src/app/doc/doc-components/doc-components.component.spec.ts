import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocComponentsComponent } from './doc-components.component';

describe('DocComponentsComponent', () => {
  let component: DocComponentsComponent;
  let fixture: ComponentFixture<DocComponentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DocComponentsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DocComponentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
