import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JointGroupAdminComponent } from './joint-group-admin.component';

describe('JointGroupAdminComponent', () => {
  let component: JointGroupAdminComponent;
  let fixture: ComponentFixture<JointGroupAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JointGroupAdminComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JointGroupAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
