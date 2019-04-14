/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ReporttypeService } from './reporttype.service';

describe('Service: Reporttype', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ReporttypeService]
    });
  });

  it('should ...', inject([ReporttypeService], (service: ReporttypeService) => {
    expect(service).toBeTruthy();
  }));
});
