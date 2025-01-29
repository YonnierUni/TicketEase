import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TicketsComponent } from './features/tickets/tickets.component';
import { MoviesComponent } from './features/movies/movies.component';
import { HomeComponent } from './features/home/home.component';
import { AdminGuard } from './guards/admin.guard';
import { ClientGuard } from './guards/client.guard';
import { UnauthorizedComponent } from './shared/components/unauthorized/unauthorized.component';
import { LoginComponent } from './shared/components/login/login.component';
import { CreateMoviesComponent } from './features/movies/create-movies/create-movies.component';
import { CreateFunctionComponent } from './features/functions/create-function/create-function.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },     
  { path: 'home', component: HomeComponent },
  { path: 'movies', component: MoviesComponent },
  { path: 'movies/create', component: CreateMoviesComponent, canActivate: [AdminGuard] },
  { path: 'tickets', component: TicketsComponent , canActivate: [ ClientGuard] },
  { path: 'functions/create', component: CreateFunctionComponent, canActivate: [AdminGuard] },
  { path: 'unauthorized', component: UnauthorizedComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', redirectTo: '/home', pathMatch: 'full' }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
