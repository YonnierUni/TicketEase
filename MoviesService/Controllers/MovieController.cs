using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketEase.Service.Movies.Entities;
using TicketEase.Service.Movies.Models;
using TicketEase.Service.Movies.Repositories;

namespace TicketEase.Service.Movies.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        // Acción para obtener todas las películas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> Get()
        {
            var movies = await _movieRepository.GetAllMoviesAsync();

            // Mapeamos la lista de entidades Movie a DTOs
            var movieDtos = _mapper.Map<IEnumerable<MovieDto>>(movies);

            return Ok(movieDtos);
        }

        // Acción para obtener una película por su ID
        [HttpGet("{movieId}")]
        public async Task<ActionResult<MovieDto>> Get(Guid movieId)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(movieId);
            if (movie == null)
            {
                return NotFound();
            }

            // Mapeamos la entidad Movie a MovieDto
            var movieDto = _mapper.Map<MovieDto>(movie);

            return Ok(movieDto);
        }

        // Acción para agregar una nueva película
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MovieDto movieDto)
        {
            var movie = _mapper.Map<Movie>(movieDto); // Convertir MovieDto a Movie entidad

            await _movieRepository.AddMovieAsync(movie);

            return CreatedAtAction(nameof(Get), new { movieId = movie.MovieId }, movieDto);
        }

        // Acción para actualizar una película
        [HttpPut("{movieId}")]
        public async Task<ActionResult> Put(Guid movieId, [FromBody] MovieDto movieDto)
        {
            if (movieId != movieDto.MovieId)
            {
                return BadRequest();
            }

            var movie = _mapper.Map<Movie>(movieDto); // Convertir MovieDto a Movie entidad

            await _movieRepository.UpdateMovieAsync(movie);
            return NoContent();
        }

        // Acción para eliminar una película
        [HttpDelete("{movieId}")]
        public async Task<ActionResult> Delete(Guid movieId)
        {
            await _movieRepository.DeleteMovieAsync(movieId);
            return NoContent();
        }
    }
}