import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TicketsComponent } from './features/tickets/tickets.component';
import { FunctionsComponent } from './features/functions/functions.component';
import { MoviesComponent } from './features/movies/movies.component';

const routes: Routes = [
  { path: 'movies', component: MoviesComponent },
  { path: 'tickets', component: TicketsComponent },
  { path: 'functions', component: FunctionsComponent },
  { path: '', redirectTo: '/movies', pathMatch: 'full' },
  { path: '**', redirectTo: '/movies', pathMatch: 'full' }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
