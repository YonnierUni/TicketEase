import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';
import { AdminGuard } from './admin.guard'; // Importa tu guardia

describe('adminGuard', () => {
  let guard: AdminGuard;
  let router: Router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule],  // Importa RouterTestingModule para las pruebas
    });
    router = TestBed.inject(Router);  // Inicia el Router
    guard = TestBed.inject(AdminGuard);  // Inicia el guard
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();  // Verifica que el guardia haya sido creado correctamente
  });
});
