using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieAPI2.Data;
using MovieAPI2.Models;

namespace MovieAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult MovieGetAll()
        {
            List<Models.Movie> movies = _context.Movies.ToList();

            if (movies == null)
                return NotFound();
            return StatusCode(200, movies);
        }

        [HttpGet("{id}")]
        public IActionResult MovieById(int movieId)
        {
            Movie? movie = _context.Movies.Where(m => m.Id == movieId).SingleOrDefault();

            if (movie == null)
                return NotFound(movie);

            return StatusCode(200, movie);
        }

        [HttpPost]
        public IActionResult MovieAdd([FromBody] Movie movie)
        {
            if (movie == null)
                return BadRequest();

            _context.Movies.Add(movie);
            _context.SaveChanges();
            return StatusCode(201, movie);
        }

        [HttpPut("{id}")]
        public IActionResult MovieUpdate(int movieId, [FromBody] Movie movieUpdt)
        {
            if (movieId <=0 || movieUpdt == null)
                return BadRequest();
           
            Movie? movie = _context.Movies.Where(m => m.Id == movieId).SingleOrDefault();

            if (movie == null)
                return NotFound(movie);
            else
            {
                movie.Title = movieUpdt.Title;
                movie.Duration = movieUpdt.Duration;
                movie.Genre = movieUpdt.Genre;
            }
            _context.SaveChanges();

            return StatusCode(200, movie);
        }


        [HttpDelete("{id}")]
        public IActionResult MovieDelete(int movieId)
        {
            Movie? movie = _context.Movies.SingleOrDefault(m => m.Id == movieId);

            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return NoContent();
        }

        

    }
}
