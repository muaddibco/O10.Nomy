import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JointMainComponent } from './joint-main.component';

describe('JointMainComponent', () => {
  let component: JointMainComponent;
  let fixture: ComponentFixture<JointMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JointMainComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JointMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
