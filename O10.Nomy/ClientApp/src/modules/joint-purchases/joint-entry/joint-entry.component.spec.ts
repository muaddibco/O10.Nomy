import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JointEntryComponent } from './joint-entry.component';

describe('JointEntryComponent', () => {
  let component: JointEntryComponent;
  let fixture: ComponentFixture<JointEntryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JointEntryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JointEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
