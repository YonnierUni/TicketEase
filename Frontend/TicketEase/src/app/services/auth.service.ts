import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = `${environment.apiBaseUrl}:7258/api/Auth`;

  private currentUserSubject: BehaviorSubject<any>;
  public currentUser: Observable<any>;

  constructor(private http: HttpClient, private router: Router) {
    const user = JSON.parse(localStorage.getItem('currentUser') || 'null');
    this.currentUserSubject = new BehaviorSubject<any>(user);
    this.currentUser = this.currentUserSubject.asObservable();
  }

  login(username: string, password: string): Observable<any> {
    return this.http
      .post<any>(`${this.apiUrl}/login`, { username, password })
      .pipe(
        tap((user) => {
          // Aquí se almacena el token y los roles
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user);
        })
      );
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    this.router.navigate(['/login']); // Redirigir a la página de login
  }

  get currentUserValue() {
    return this.currentUserSubject.value;
  }

  isAuthenticated(): boolean {
    // Devuelve si hay un usuario autenticado o no
    return !!this.currentUserSubject.value;
  }

  // Verificación de roles
  isAdmin(): boolean {
    return (
      this.currentUserSubject.value &&
      this.currentUserSubject.value.role === 'admin'
    );
  }

  isClient(): boolean {
    return (
      this.currentUserSubject.value &&
      this.currentUserSubject.value.role === 'client'
    );
  }
}
