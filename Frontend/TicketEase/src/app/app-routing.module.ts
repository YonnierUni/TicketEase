import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TicketsComponent } from './features/tickets/tickets.component';
import { FunctionsComponent } from './features/functions/functions.component';
import { MoviesComponent } from './features/movies/movies.component';
import { HomeComponent } from './features/home/home.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'movies', component: MoviesComponent },
  { path: 'tickets', component: TicketsComponent },
  { path: 'functions/:movieId', component: FunctionsComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', redirectTo: '/home', pathMatch: 'full' }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
