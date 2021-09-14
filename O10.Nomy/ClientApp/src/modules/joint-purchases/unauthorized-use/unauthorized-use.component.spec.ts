import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnauthorizedUseComponent } from './unauthorized-use.component';

describe('UnauthorizedUseComponent', () => {
  let component: UnauthorizedUseComponent;
  let fixture: ComponentFixture<UnauthorizedUseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnauthorizedUseComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UnauthorizedUseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
