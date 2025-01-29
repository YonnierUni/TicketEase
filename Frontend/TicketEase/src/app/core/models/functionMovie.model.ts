export interface FunctionMovieDto {
  movieFunctionId: string;
  movieId: string;
  room: string;
  startTime: string;
  endTime: string;
  totalSeats: number;
  availableSeats: number;
}

export interface FunctionForCreationDto {
  movieId: string;
  price: number;
  functionDate: Date;
}

export interface FunctionDto {
  functionId: string;
  movieId: string;
  price: number;
  functionDate: string;
}
