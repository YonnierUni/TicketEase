export interface TicketDto {
    ticketId: string;
    functionId: string;
    additionalPrice: number;
    totalPrice: number;
    userName: string;
  }
  
  export interface TicketForCreationDto {
    functionId: string;
    additionalPrice: number;
    userName: string;
  }
  
  export interface TicketForUpdateDto {
    userName: string;
  }
  