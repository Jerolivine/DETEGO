/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { LoginGuardService } from './loginGuard.service';

describe('Service: LoginGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LoginGuardService]
    });
  });

  it('should ...', inject([LoginGuardService], (service: LoginGuardService) => {
    expect(service).toBeTruthy();
  }));
});
