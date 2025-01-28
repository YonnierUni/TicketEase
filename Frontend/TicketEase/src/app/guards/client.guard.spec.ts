import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';
import { ClientGuard } from './client.guard';

describe('clientGuard', () => {
  let guard: ClientGuard;
  let router: Router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule],
    });
    router = TestBed.inject(Router);
    guard = TestBed.inject(ClientGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
