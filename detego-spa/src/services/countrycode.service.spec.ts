/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CountrycodeService } from './countrycode.service';

describe('Service: Countrycode', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CountrycodeService]
    });
  });

  it('should ...', inject([CountrycodeService], (service: CountrycodeService) => {
    expect(service).toBeTruthy();
  }));
});
