import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocIndexComponent } from './doc-index.component';

describe('DocIndexComponent', () => {
  let component: DocIndexComponent;
  let fixture: ComponentFixture<DocIndexComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DocIndexComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DocIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
