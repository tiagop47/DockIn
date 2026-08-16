import { TestBed } from '@angular/core/testing';

import { Artigos } from './artigos';

describe('Artigos', () => {
  let service: Artigos;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Artigos);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
