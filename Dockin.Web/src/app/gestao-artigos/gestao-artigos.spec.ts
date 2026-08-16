import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GestaoArtigos } from './gestao-artigos';

describe('GestaoArtigos', () => {
  let component: GestaoArtigos;
  let fixture: ComponentFixture<GestaoArtigos>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GestaoArtigos],
    }).compileComponents();

    fixture = TestBed.createComponent(GestaoArtigos);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
