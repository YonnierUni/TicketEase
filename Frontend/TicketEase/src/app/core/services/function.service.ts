import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../src/environments/environment';
import { FunctionMovieDto } from '../models/functionMovie.model';

@Injectable({
  providedIn: 'root'
})
export class FunctionService {

  private apiUrl = `${environment.apiBaseUrl}/api/functions`;

  constructor(private http: HttpClient) { }

  getFunctions(): Observable<FunctionMovieDto[]> {
    return this.http.get<FunctionMovieDto[]>(this.apiUrl);
  }

  getFunctionById(functionId: string): Observable<FunctionMovieDto> {
    return this.http.get<FunctionMovieDto>(`${this.apiUrl}/${functionId}`);
  }

  createFunction(functionData: FunctionMovieDto): Observable<FunctionMovieDto> {
    return this.http.post<FunctionMovieDto>(this.apiUrl, functionData);
  }

  updateFunction(functionId: string, functionData: FunctionMovieDto): Observable<FunctionMovieDto> {
    return this.http.put<FunctionMovieDto>(`${this.apiUrl}/${functionId}`, functionData);
  }

  deleteFunction(functionId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${functionId}`);
  }
}
