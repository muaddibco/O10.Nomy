import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RevealSecretsComponent } from './reveal-secrets.component';

describe('RevealSecretsComponent', () => {
  let component: RevealSecretsComponent;
  let fixture: ComponentFixture<RevealSecretsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RevealSecretsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RevealSecretsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
