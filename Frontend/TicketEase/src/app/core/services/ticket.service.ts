import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../src/environments/environment';
import { CancelTicketsRequest, TicketDto, TicketForCreationDto, TicketForUpdateDto } from '../models/ticket.model';

@Injectable({
  providedIn: 'root'
})
export class TicketService {

  private apiUrl = `${environment.apiBaseUrl}:7134/api/tickets`;

  constructor(private http: HttpClient) { }

  getTickets(): Observable<TicketDto[]> {
    return this.http.get<TicketDto[]>(this.apiUrl);
  }

  getTicketById(ticketId: string): Observable<TicketDto> {
    return this.http.get<TicketDto>(`${this.apiUrl}/${ticketId}`);
  }

  createTicket(ticket: TicketForCreationDto): Observable<TicketDto> {
    return this.http.post<TicketDto>(`${this.apiUrl}/addTicket`, ticket);
  }
  
  createMultipleTickets(tickets: TicketForCreationDto[]): Observable<TicketDto[]> {
    return this.http.post<TicketDto[]>(`${this.apiUrl}/addMultipleTickets`, tickets);
  }
  
  updateTicket(ticketId: string, ticket: TicketForUpdateDto): Observable<TicketDto> {
    return this.http.put<TicketDto>(`${this.apiUrl}/${ticketId}`, ticket);
  }

  deleteTicket(ticketId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${ticketId}`);
  }

  cancelTickets(cancelRequest: CancelTicketsRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/cancelTickets`, cancelRequest);
  }

  getTicketsByFunctionId(functionId: string): Observable<TicketDto[]> {
    return this.http.get<TicketDto[]>(`${this.apiUrl}/function/${functionId}`);
  }

  getTicketsByUserName(userName: string): Observable<TicketDto[]> {
    return this.http.get<TicketDto[]>(`${this.apiUrl}/user/${userName}`);
  }
}
