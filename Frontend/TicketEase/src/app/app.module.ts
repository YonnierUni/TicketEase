import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { TicketsComponent } from './features/tickets/tickets.component';
import { FunctionsComponent } from './features/functions/functions.component';
import { MoviesComponent } from './features/movies/movies.component';
import { provideHttpClient  } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './features/home/home.component';
import { LoginComponent } from './shared/components/login/login.component';
import { UnauthorizedComponent } from './shared/components/unauthorized/unauthorized.component';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { CreateMoviesComponent } from './features/movies/create-movies/create-movies.component';
import { CreateFunctionComponent } from './features/functions/create-function/create-function.component';

@NgModule({
  declarations: [
    AppComponent,
    TicketsComponent,
    FunctionsComponent,
    MoviesComponent,
    HomeComponent,
    LoginComponent,
    UnauthorizedComponent,
    NavbarComponent,
    CreateMoviesComponent,
    CreateFunctionComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule, 
    FormsModule,
    ReactiveFormsModule,
    CommonModule
  ],
  providers: [
    provideHttpClient(),
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
