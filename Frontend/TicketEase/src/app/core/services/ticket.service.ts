import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../src/environments/environment';
import {
  CancelTicketsRequest,
  TicketDto,
  TicketForCreationDto,
  TicketForUpdateDto,
} from '../models/ticket.model';
import { FunctionDto, FunctionForCreationDto } from '../models/functionMovie.model';

@Injectable({
  providedIn: 'root',
})
export class TicketService {
  //private apiUrl = `${environment.apiBaseUrl}:7134/api/tickets`;
  //private apiUrlFunctions = `${environment.apiBaseUrl}:7134/api/functions`;
  private apiUrl = `${environment.apiBaseUrl}/ticketpurchase/api/tickets`;
  private apiUrlFunctions = `${environment.apiBaseUrl}/ticketpurchase/api/functions`;

  constructor(private http: HttpClient) {}

  getTickets(): Observable<TicketDto[]> {
    return this.http.get<TicketDto[]>(this.apiUrl);
  }

  getTicketById(ticketId: string): Observable<TicketDto> {
    return this.http.get<TicketDto>(`${this.apiUrl}/${ticketId}`);
  }

  createTicket(ticket: TicketForCreationDto): Observable<TicketDto> {
    return this.http.post<TicketDto>(`${this.apiUrl}/addTicket`, ticket);
  }

  createMultipleTickets(
    tickets: TicketForCreationDto[]
  ): Observable<TicketDto[]> {
    return this.http.post<TicketDto[]>(
      `${this.apiUrl}/addMultipleTickets`,
      tickets
    );
  }

  updateTicket(
    ticketId: string,
    ticket: TicketForUpdateDto
  ): Observable<TicketDto> {
    return this.http.put<TicketDto>(`${this.apiUrl}/${ticketId}`, ticket);
  }

  deleteTicket(ticketId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${ticketId}`);
  }

  cancelTickets(cancelRequest: CancelTicketsRequest): Observable<any> {
    const currentUser = JSON.parse(localStorage.getItem('currentUser') || '{}'); // Si currentUser está guardado en localStorage
    const token = currentUser.token;
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.apiUrl}/cancelTickets`, cancelRequest, {
      headers,
    });
  }

  getTicketsByFunctionId(functionId: string): Observable<TicketDto[]> {
    return this.http.get<TicketDto[]>(`${this.apiUrl}/function/${functionId}`);
  }

  getTicketsByUserName(userName: string): Observable<TicketDto[]> {
    return this.http.get<TicketDto[]>(`${this.apiUrl}/user/${userName}`);
  }

  createFunction(functionDto: FunctionForCreationDto): Observable<FunctionDto> {
    return this.http.post<FunctionDto>(`${this.apiUrlFunctions}`, functionDto);
  }
}
