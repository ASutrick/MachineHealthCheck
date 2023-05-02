import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckHistoryModalComponent } from './check-history-modal.component';

describe('CheckHistoryModalComponent', () => {
  let component: CheckHistoryModalComponent;
  let fixture: ComponentFixture<CheckHistoryModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CheckHistoryModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CheckHistoryModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
