import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { TicketsComponent } from './features/tickets/tickets.component';
import { FunctionsComponent } from './features/functions/functions.component';
import { MoviesComponent } from './features/movies/movies.component';

@NgModule({
  declarations: [
    AppComponent,
    TicketsComponent,
    FunctionsComponent,
    MoviesComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
