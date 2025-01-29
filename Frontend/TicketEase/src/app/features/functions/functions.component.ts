import { Component, Input, SimpleChanges } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable, Subscription } from 'rxjs';
import { FunctionMovieDto } from '../../core/models/functionMovie.model';
import { FunctionService } from '../../core/services/function.service';
import { TicketService } from '../../core/services/ticket.service';
import { TicketDto, TicketForCreationDto } from '../../core/models/ticket.model';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-functions',
  standalone: false,

  templateUrl: './functions.component.html',
  styleUrl: './functions.component.css',
})
export class FunctionsComponent {
  @Input() movieId: string | null = null; // Aceptar movieId como parámetro
  isLoggedIn = false;

  private apiUrl = `${environment.apiBaseUrl}/api/functions`;
  movieFunctions: FunctionMovieDto[] = []; // Array para almacenar las funciones de la película
  selectedFunction: FunctionMovieDto | null = null; // Función seleccionada para la compra
  ticketQuantity: number = 1; // Cantidad de boletos que desea comprar
  private subscriptions: Subscription = new Subscription();

  constructor(
    private functionService: FunctionService,
    private ticketService: TicketService,
    private router: Router,
    private authService: AuthService
    
  ) {}
  
  ngOnInit(): void {
    // Escuchar cambios en currentUser (estado de autenticación)
    this.subscriptions.add(
      this.authService.currentUser.subscribe((user) => {
        this.isLoggedIn = !!user; // Si el user existe, está logueado
      })
    );
  }

  getFunctionsByMovie(movieId: string): void {
    this.functionService.getFunctionsByMovie(movieId).subscribe((functions) => {
      this.movieFunctions = functions;
    });
  }

  // Detecta cambios en el movieId recibido por Input y realiza la petición
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['movieId'] && this.movieId) {
      this.getFunctionsByMovie(this.movieId);
    }
  }

  // Método para seleccionar una función
  selectFunction(func: FunctionMovieDto): void {
    this.selectedFunction = func;
    this.ticketQuantity = 1; // Resetear cantidad de boletos al seleccionar nueva función
  }

  // Método para manejar la compra de boletos
  purchaseTickets(): void {
    if (
      this.selectedFunction &&
      this.ticketQuantity <= this.selectedFunction.availableSeats
    ) {
      // Crear el arreglo de tickets para enviar al servicio
      const ticketsArray: TicketForCreationDto[] = [];
      
      for (let i = 0; i < this.ticketQuantity; i++) {
        // Crear un ticket por cada cantidad seleccionada
        const newTicket: TicketForCreationDto = {
          functionId: this.selectedFunction.movieFunctionId, // Id de la función
          additionalPrice: 0,  // Precio adicional (ajustar este campo según necesidad)
          userName: environment.email,
        };
  
        ticketsArray.push(newTicket);
      }
  
      // Llamar al servicio para crear múltiples tickets
      this.ticketService.createMultipleTickets(ticketsArray).subscribe({
        next: (createdTickets: TicketDto[]) => {
          // Si la compra es exitosa, mostramos los resultados
          alert(`Compra confirmada: ${this.ticketQuantity} boletos para la función en la sala ${this.selectedFunction?.room}.`);
  
          // Actualizamos los asientos disponibles y otras variables

          this.ticketQuantity = 1; // Reiniciar cantidad
          this.selectedFunction = null; // Deseleccionar la función
          this.router.navigate(['/tickets']);
        },
        error: (err) => {
          console.error('Error al crear los tickets:', err);
          alert('Ocurrió un error al procesar la compra. Por favor, inténtalo nuevamente.');
        },
      });
    } else {
      alert('No hay suficientes asientos disponibles.');
    }
  }

  updateAvailableSeats(functionId: string): void {
    // Llamamos al servicio de función para obtener los detalles de la función
    this.functionService.getFunctionById(functionId).subscribe({
      next: (functionData) => {
        // Reducimos los asientos disponibles por la cantidad de tickets comprados
        functionData.availableSeats -= this.ticketQuantity;

        // Actualizamos los asientos disponibles en la función
        this.functionService.updateFunction(functionId, functionData).subscribe({
          next: (updatedFunction) => {
            console.log('Asientos actualizados exitosamente');
          },
          error: (err) => {
            console.error('Error al actualizar los asientos disponibles:', err);
          },
        });
      },
      error: (err) => {
        console.error('Error al obtener los detalles de la función:', err);
      },
    });
  }
}
