import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TicketsComponent } from './features/tickets/tickets.component';
import { FunctionsComponent } from './features/functions/functions.component';
import { MoviesComponent } from './features/movies/movies.component';
import { HomeComponent } from './features/home/home.component';
import { AdminGuard } from './guards/admin.guard';
import { ClientGuard } from './guards/client.guard';
import { UnauthorizedComponent } from './shared/components/unauthorized/unauthorized.component';
import { LoginComponent } from './shared/components/login/login.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },     
  { path: 'home', component: HomeComponent },
  { path: 'movies', component: MoviesComponent, canActivate: [ClientGuard] },
  { path: 'tickets', component: TicketsComponent , canActivate: [AdminGuard] },
  { path: 'functions/:movieId', component: FunctionsComponent },
  { path: 'unauthorized', component: UnauthorizedComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login', pathMatch: 'full' }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
