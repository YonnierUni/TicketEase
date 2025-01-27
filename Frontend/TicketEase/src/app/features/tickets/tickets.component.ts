import { Component, OnInit } from '@angular/core';
import { TicketService } from '../../core/services/ticket.service';
import { TicketDto } from '../../core/models/ticket.model';
//import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.css'],
  //imports: [CommonModule]
})
export class TicketsComponent implements OnInit {
  ngOnInit(): void {
  }
  /*
  tickets: TicketDto[] = [];

  constructor(private ticketService: TicketService) {}

  ngOnInit(): void {
    this.loadTickets();
  }

  loadTickets(): void {
    this.ticketService.getTickets().subscribe(
      (data) => this.tickets = data,
      (error) => console.error('Error loading tickets', error)
    );
  }

  deleteTicket(ticketId: string): void {
    this.ticketService.deleteTicket(ticketId).subscribe(() => {
      this.tickets = this.tickets.filter(ticket => ticket.ticketId !== ticketId);
    });
  }

  // Otros métodos como agregar o actualizar tickets van aquí
  */
}
