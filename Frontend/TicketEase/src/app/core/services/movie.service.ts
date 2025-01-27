import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../src/environments/environment';
import { MovieDto } from '../models/movie.model';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  private apiUrl = `${environment.apiBaseUrl}/api/movies`;

  constructor(private http: HttpClient) { }

  getMovies(): Observable<MovieDto[]> {
    return this.http.get<MovieDto[]>(this.apiUrl);
  }

  getMovieById(movieId: string): Observable<MovieDto> {
    return this.http.get<MovieDto>(`${this.apiUrl}/${movieId}`);
  }

  createMovie(movieData: MovieDto): Observable<MovieDto> {
    return this.http.post<MovieDto>(this.apiUrl, movieData);
  }

  updateMovie(movieId: string, movieData: MovieDto): Observable<MovieDto> {
    return this.http.put<MovieDto>(`${this.apiUrl}/${movieId}`, movieData);
  }

  deleteMovie(movieId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${movieId}`);
  }
}
