import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JointGroupComponent } from './joint-group.component';

describe('JointGroupComponent', () => {
  let component: JointGroupComponent;
  let fixture: ComponentFixture<JointGroupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JointGroupComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JointGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
