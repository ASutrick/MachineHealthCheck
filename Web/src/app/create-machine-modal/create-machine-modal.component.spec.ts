import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateMachineModalComponent } from './create-machine-modal.component';

describe('CreateMachineModalComponent', () => {
  let component: CreateMachineModalComponent;
  let fixture: ComponentFixture<CreateMachineModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateMachineModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateMachineModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
