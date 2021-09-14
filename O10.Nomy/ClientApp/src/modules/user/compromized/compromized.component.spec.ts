import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompromizedComponent } from './compromized.component';

describe('CompromizedComponent', () => {
  let component: CompromizedComponent;
  let fixture: ComponentFixture<CompromizedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompromizedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompromizedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
