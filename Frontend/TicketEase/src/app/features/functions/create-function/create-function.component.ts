import { Component, OnInit } from '@angular/core';
import {
  FunctionForCreationDto,
  FunctionMovieDto,
} from '../../../core/models/functionMovie.model';
import { MovieDto } from '../../../core/models/movie.model';
import { MovieService } from '../../../core/services/movie.service';
import { FunctionService } from '../../../core/services/function.service';
import { TicketService } from '../../../core/services/ticket.service';

@Component({
  selector: 'app-create-function',
  standalone: false,

  templateUrl: './create-function.component.html',
  styleUrl: './create-function.component.css',
})
export class CreateFunctionComponent implements OnInit {
  movies: MovieDto[] = [];

  selectedMovieId: string | null = null; // Película seleccionada
  price: number | null = null;

  functionMovie: Partial<FunctionMovieDto> = {
    room: '',
    startTime: '',
    endTime: '',
    totalSeats: 0,
    availableSeats: 0,
  };
  selectedMovieFunctions: FunctionMovieDto[] = []; // Lista de funciones

  constructor(
    private movieService: MovieService,
    private functionService: FunctionService,
    private ticketService: TicketService
  ) {}

  ngOnInit(): void {
    this.getMovies();
  }

  getMovies(): void {
    this.movieService.getMovies().subscribe((data: MovieDto[]) => {
      this.movies = data;
    });
  }

  getFunctionsByMovie(): void {
    if (!this.selectedMovieId) {
      this.selectedMovieFunctions = [];
      return;
    }

    this.functionService.getFunctionsByMovie(this.selectedMovieId).subscribe(
      (functions) => {
        this.selectedMovieFunctions = functions || [];
      },
      (error) => {
        console.error('Error fetching functions:', error);
        this.selectedMovieFunctions = []; // En caso de error, limpiar la lista
      }
    );
  }

  // Valida el formulario antes de agregar una función
  isValidFunction(): boolean {
    return (
      this.functionMovie.room != null &&
      this.functionMovie.startTime != null &&
      this.functionMovie.endTime != null &&
      this.functionMovie.totalSeats != null &&
      this.functionMovie.totalSeats > 0
    );
  }

  // Agrega una nueva función para la película seleccionada
  addFunction(): void {
    if (!this.selectedMovieId || !this.isValidFunction()) return;

    // Crear el objeto para el servicio de TicketEase
    const functionForTicket: FunctionForCreationDto = {
      movieId: this.selectedMovieId,
      price: this.price!, // Precio necesario según FunctionForCreationDto
      functionDate: new Date(this.functionMovie.startTime!), // Fecha inicial de la función
    };

    // Primero, crear la función en TicketEase
    this.ticketService.createFunction(functionForTicket).subscribe({
      next: (ticketFunction) => {
        console.log(
          'Function created successfully in TicketService',
          ticketFunction
        );

        const newFunction: FunctionMovieDto = {
          movieFunctionId: ticketFunction.functionId, // ID generado en TicketEase
          movieId: ticketFunction.movieId,
          room: this.functionMovie.room!,
          startTime: this.functionMovie.startTime!,
          endTime: this.functionMovie.endTime!,
          totalSeats: this.functionMovie.totalSeats!,
          availableSeats: this.functionMovie.totalSeats!,
        };

        // Ahora crear la función en FunctionService
        this.functionService.createFunction(newFunction).subscribe({
          next: () => {
            console.log('Function successfully created in both services!');
            alert(`Function successfully created.`);
            this.getFunctionsByMovie();
            this.resetForm();
          },
          error: (err) => {
            console.error('Error creating function in FunctionService:', err);
              alert(`Error creating function.`);
          },
        });
      },
      error: (err) =>{
        console.error('Error creating function in TicketService:', err);
        alert(`Error creating functionn.`);
      }
    });
  }

  // Reinicia el formulario después de agregar una función
  resetForm(): void {
    this.functionMovie = {
      room: '',
      startTime: '',
      endTime: '',
      totalSeats: 0,
      availableSeats: 0,
    };
    this.price = 0;
  }
}
