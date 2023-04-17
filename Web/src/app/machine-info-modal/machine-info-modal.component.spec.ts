import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MachineInfoModalComponent } from './machine-info-modal.component';

describe('MachineInfoModalComponent', () => {
  let component: MachineInfoModalComponent;
  let fixture: ComponentFixture<MachineInfoModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MachineInfoModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MachineInfoModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
