using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketEase.Service.Movies.Entities;
using TicketEase.Service.Movies.Models;
using TicketEase.Service.Movies.Repositories;
using TicketEase.Service.Movies.Services;

namespace TicketEase.Service.Movies.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public MovieController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> Get()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return Ok(movies);
        }

        [HttpGet("{movieId}")]
        public async Task<ActionResult<MovieDto>> Get(Guid movieId)
        {
            try
            {
                var movie = await _movieService.GetMovieByIdAsync(movieId);
                return Ok(movie);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpPost("getMoviesByIds")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMoviesByIds([FromBody] List<Guid> movieIds)
        {
            try
            {
                var movies = await _movieService.GetMoviesByIdsAsync(movieIds);
                return Ok(movies);
            }
            catch (Exception)
            {
                return BadRequest("Error al obtener las películas.");
            }
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MovieDto movieDto)
        {
            await _movieService.AddMovieAsync(movieDto);
            return CreatedAtAction(nameof(Get), new { movieId = movieDto.MovieId }, movieDto);
        }

        [HttpPut("{movieId}")]
        public async Task<ActionResult> Put(Guid movieId, [FromBody] MovieDto movieDto)
        {
            try
            {
                await _movieService.UpdateMovieAsync(movieId, movieDto);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{movieId}")]
        public async Task<ActionResult> Delete(Guid movieId)
        {
            await _movieService.DeleteMovieAsync(movieId);
            return NoContent();
        }
    }
}