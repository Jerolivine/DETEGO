/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { StoreaddComponent } from './storeadd.component';

describe('StoreaddComponent', () => {
  let component: StoreaddComponent;
  let fixture: ComponentFixture<StoreaddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StoreaddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StoreaddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
