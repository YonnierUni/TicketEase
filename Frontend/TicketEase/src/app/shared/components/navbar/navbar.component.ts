import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-navbar',
  standalone: false,
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit, OnDestroy {
  isLoggedIn = false;
  userName: string | null = null;
  userRol: string | null = null;
  private subscriptions: Subscription = new Subscription();

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    // Escuchar cambios en currentUser (estado de autenticación)
    this.subscriptions.add(
      this.authService.currentUser.subscribe((user) => {
        this.isLoggedIn = !!user; // Si el user existe, está logueado
        this.userName = user.name ? user.name : null; // Se asigna el nombre o null si no está logueado
        this.userRol = user.role ? user.role : null;
      })
    );
  }

  logout(): void {
    this.authService.logout();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe(); // Desuscribirse cuando el componente se destruya para evitar memory leaks
  }
}
