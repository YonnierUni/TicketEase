import { Component } from '@angular/core';
import { TicketService } from '../../core/services/ticket.service';
import {
  CancelTicketsRequest,
  TicketDto,
  TicketWithDetailsDto,
} from '../../core/models/ticket.model';
import { environment } from '../../../environments/environment';
import { FunctionService } from '../../core/services/function.service';
import { MovieService } from '../../core/services/movie.service';
import { MovieDto } from '../../core/models/movie.model';

@Component({
  selector: 'app-tickets',
  standalone: false,

  templateUrl: './tickets.component.html',
  styleUrl: './tickets.component.css',
})
export class TicketsComponent {
  purchasedTickets: TicketWithDetailsDto[] = [];
  movies: MovieDto[] = [];
  selectedTicket: TicketWithDetailsDto | null = null;
  ticketServiceList: TicketDto[] = [];

  userName = environment.email;
  isLoading = true;

  constructor(
    private ticketService: TicketService,
    private functionService: FunctionService,
    private movieService: MovieService
  ) {}

  ngOnInit(): void {
    this.getTicketsForCustomerWithDetails();
  }

  getTicketsForCustomerWithDetails(): void {
    // Paso 1: Obtener los boletos por usuario
    this.ticketService.getTicketsByUserName(this.userName).subscribe(
      (tickets) => {
        this.ticketServiceList = tickets;
        // Paso 2: Extraer todos los functionIds
        const functionIds = tickets.map((ticket) => ticket.functionId);

        // Paso 3: Consultar las funciones correspondientes a esos functionIds
        this.functionService.getFunctionsByIds(functionIds).subscribe(
          (functions) => {
            // Paso 4: Obtener todos los movieIds desde las funciones
            const movieIds = functions.map((fn) => fn.movieId);

            // Paso 5: Consultar las películas asociadas a esos movieIds
            this.movieService.getMoviesByIds(movieIds).subscribe(
              (movies) => {
                this.movies = movies;

                // Agrupamos los tickets por functionId y movieName
                const groupedTickets = tickets.reduce((acc, ticket) => {
                  const functionInfo = functions.find(
                    (fn) => fn.movieFunctionId === ticket.functionId
                  );
                  const movieInfo = movies.find(
                    (movie: MovieDto) => movie.movieId === functionInfo?.movieId
                  );

                  const key = `${ticket.functionId}-${movieInfo?.title}`;
                  if (!acc[key]) {
                    acc[key] = {
                      functionId: functionInfo
                        ? functionInfo.movieFunctionId
                        : 'Desconocido',
                      ticketId: ticket.ticketId
                        ? ticket.ticketId
                        : 'Desconocido',
                      movieName: movieInfo ? movieInfo.title : 'Desconocido',
                      functionRoom: functionInfo
                        ? functionInfo.room
                        : 'Desconocido',
                      startTime: functionInfo
                        ? new Date(functionInfo.startTime)
                        : new Date(),
                      ticketQuantity: 0,
                    };
                  }
                  acc[key].ticketQuantity++;
                  return acc;
                }, {} as Record<string, TicketWithDetailsDto>);

                // Convertimos el objeto agrupado en un array
                this.purchasedTickets = Object.values(groupedTickets);

                this.isLoading = false;
              },
              (movieError) => {
                console.error('Error al obtener las películas', movieError);
                this.isLoading = false;
              }
            );
          },
          (functionError) => {
            console.error('Error al obtener las funciones', functionError);
            this.isLoading = false;
          }
        );
      },
      (error) => {
        console.error('Error al obtener los boletos', error);
        this.isLoading = false;
      }
    );
  }

  viewTicketDetails(ticket: TicketWithDetailsDto): void {
    this.selectedTicket = ticket; // Asigna el ticket seleccionado
  }

  closeModal(): void {
    this.selectedTicket = null; // Cierra el modal al hacer clic
  }

  cancelTicket(ticket: TicketWithDetailsDto): void {
    const maxTicketsToCancel = ticket.ticketQuantity;
    const input = prompt(
      `Has comprado ${maxTicketsToCancel} boletos para "${ticket.movieName}".\n¿Cuántos boletos deseas cancelar?`
    );

    const ticketsToCancel = parseInt(input || '0', 10);

    if (isNaN(ticketsToCancel) || ticketsToCancel <= 0) {
      alert('Por favor, introduce un número válido.');
      return;
    }

    if (ticketsToCancel > maxTicketsToCancel) {
      alert(`No puedes cancelar más de ${maxTicketsToCancel} boletos.`);
      return;
    }

    // Construir el objeto de solicitud de cancelación
    const cancelRequest: CancelTicketsRequest = {
      ticketIds: this.ticketServiceList
        .filter((tickettt) => tickettt.functionId === ticket.functionId)
        .map((tickettt) => tickettt.ticketId),
    };

    this.ticketService.cancelTickets(cancelRequest).subscribe(
      (response) => {
        const ticketIndex = this.purchasedTickets.findIndex(
          (t) => t.ticketId === ticket.ticketId
        );

        if (ticketIndex !== -1) {
          const targetTicket = this.purchasedTickets[ticketIndex];

          // Actualiza la cantidad de tickets
          if (ticketsToCancel < maxTicketsToCancel) {
            this.purchasedTickets[ticketIndex].ticketQuantity -=
              ticketsToCancel;
          } else {
            // Si cancela todos los tickets, elimina la entrada
            this.purchasedTickets.splice(ticketIndex, 1);
          }
        }

        alert(
          `${ticketsToCancel} boletos para "${ticket.movieName}" han sido cancelados exitosamente.`
        );
      },
      (error) => {
        alert(
          `Ocurrió un error al intentar cancelar los boletos: ${error.message}`
        );
      }
    );
  }
}
