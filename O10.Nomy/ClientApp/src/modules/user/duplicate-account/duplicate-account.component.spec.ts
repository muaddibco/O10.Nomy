import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DuplicateAccountComponent } from './duplicate-account.component';

describe('DuplicateAccountComponent', () => {
  let component: DuplicateAccountComponent;
  let fixture: ComponentFixture<DuplicateAccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DuplicateAccountComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DuplicateAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
