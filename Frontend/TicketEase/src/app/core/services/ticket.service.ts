import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../src/environments/environment';
import { TicketDto, TicketForCreationDto, TicketForUpdateDto } from '../models/ticket.model';

@Injectable({
  providedIn: 'root'
})
export class TicketService {

  private apiUrl = `${environment.apiBaseUrl}/api/tickets`;

  constructor(private http: HttpClient) { }

  getTickets(): Observable<TicketDto[]> {
    return this.http.get<TicketDto[]>(this.apiUrl);
  }

  getTicketById(ticketId: string): Observable<TicketDto> {
    return this.http.get<TicketDto>(`${this.apiUrl}/${ticketId}`);
  }

  createTicket(ticket: TicketForCreationDto): Observable<TicketDto> {
    return this.http.post<TicketDto>(this.apiUrl, ticket);
  }

  updateTicket(ticketId: string, ticket: TicketForUpdateDto): Observable<TicketDto> {
    return this.http.put<TicketDto>(`${this.apiUrl}/${ticketId}`, ticket);
  }

  deleteTicket(ticketId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${ticketId}`);
  }

  getTicketsByFunctionId(functionId: string): Observable<TicketDto[]> {
    return this.http.get<TicketDto[]>(`${this.apiUrl}/function/${functionId}`);
  }

  getTicketsByUserName(userName: string): Observable<TicketDto[]> {
    return this.http.get<TicketDto[]>(`${this.apiUrl}/user/${userName}`);
  }
}
