import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: false,

  templateUrl: './login.component.html',  // Asegúrate de crear este archivo .html
  styleUrls: ['./login.component.css']  // O usa el .scss si estás utilizando SCSS
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    this.authService.login(this.username, this.password)
      .subscribe(
        user => {
          if (this.authService.isAdmin()) {
            this.router.navigate(['/tickets']);
          } else if (this.authService.isClient()) {
            this.router.navigate(['/movies']);
          }
        },
        error => {
          alert('Login failed');
        }
      );
  }
}
