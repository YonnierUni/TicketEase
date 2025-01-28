import { Component, OnInit } from '@angular/core';
import { MovieService } from '../../core/services/movie.service';
import { MovieDto } from '../../core/models/movie.model';
import { FunctionMovieDto } from '../../core/models/functionMovie.model';
import { FunctionService } from '../../core/services/function.service';
import { TicketService } from '../../core/services/ticket.service';

@Component({
  selector: 'app-movies',
  standalone: false,

  templateUrl: './movies.component.html',
  styleUrl: './movies.component.css',
})
export class MoviesComponent implements OnInit {
  movies: MovieDto[] = [];
  selectedMovie: MovieDto | null = null;
  ticketQuantity: number = 1;
  totalPrice: number = 0;
  userName: string = '';
  selectedMovieId: string = ''; // Un ejemplo de un movieId fijo o variable

  constructor(private movieService: MovieService) {}

  ngOnInit(): void {
    this.getMovies();
  }
  /*
  getMovies(): void {
      this.movies = [
        {
          movieId: '7A573BF8-D473-4291-B221-B39B7CA415C9',
          title: "Avengers: Endgame",
          year: 2019,
          genre: "Action",
          director: "Anthony Russo, Joe Russo",
          cast: "Robert Downey Jr., Chris Evans, Scarlett Johansson",
          posterImage: "endgame.jpg"
        },
        {
          movieId: 'D12F5F9E-05A3-4742-8ECF-174C4AE57527',
          title: "Spider-Man: No Way Home",
          year: 2021,
          genre: "Action, Sci-Fi",
          director: "Jon Watts",
          cast: "Tom Holland, Zendaya, Benedict Cumberbatch",
          posterImage: "spiderman_no_way_home.jpg"
        }
      ];;
  }

  selectMovie(movie: MovieDto): void {
    this.selectedMovie = movie;
    // Datos simulados para las funciones de películas
    this.movieFunctions = [
      {
        movieFunctionId: '1',
        movieId: movie.movieId,
        room: 'Room 1',
        startTime: '2025-03-01T14:00:00',
        endTime: '2025-03-01T16:00:00',
        totalSeats: 150,
        availableSeats: 100,
      },
      {
        movieFunctionId: '2',
        movieId: movie.movieId,
        room: 'Room 2',
        startTime: '2025-03-01T17:00:00',
        endTime: '2025-03-01T19:00:00',
        totalSeats: 150,
        availableSeats: 130,
      }
    ];
  }
*/
  getMovies(): void {
    this.movieService.getMovies().subscribe((data: MovieDto[]) => {
      this.movies = data;
    });
  }
  selectMovie(movie: MovieDto): void {
    this.selectedMovie = movie;
    this.selectedMovieId = movie.movieId;
  }
  closeFunctions(): void {
    this.selectedMovie = null;
    this.selectedMovieId = "";
  }
}
