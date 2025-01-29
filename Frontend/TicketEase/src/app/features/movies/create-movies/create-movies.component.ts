import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MovieService } from '../../../core/services/movie.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-movies',
  standalone: false,
  
  templateUrl: './create-movies.component.html',
  styleUrl: './create-movies.component.css'
})
export class CreateMoviesComponent {
  movieForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private movieService: MovieService,
    private router: Router
  ) {
    this.movieForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(100)]],
      year: [
        '',
        [Validators.required, Validators.min(1900), Validators.max(new Date().getFullYear())],
      ],
      genre: ['', Validators.required],
      director: ['', [Validators.required, Validators.maxLength(100)]],
      cast: ['', [Validators.required, Validators.maxLength(500)]],
      posterImage: ['', Validators.required]
    });
  }

  createMovie(): void {
    if (this.movieForm.invalid) {
      return; // No enviar si el formulario no es válido
    }

    this.movieService.createMovie(this.movieForm.value).subscribe({
      next: () => {
        alert('Movie created successfully!');
        this.router.navigate(['/movies']);
      },
      error: (err) => {
        console.error('Error creating movie:', err);
        alert('Failed to create movie. Please try again later.');
      }
    });
  }
}