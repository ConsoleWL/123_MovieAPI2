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
            // what is the best return type would be for this particular situation and what type of return type are not recommended
            return StatusCode(200, movies);
        }

        [HttpGet("{id}")]
        public IActionResult MovieById(int movieId)
        {
            // linq reusts .. what type of Linq requests  also can fit in here. Which one should be avoided.
            Movie? movie = _context.Movies.Where(m => m.Id == movieId).SingleOrDefault();

            if (movie == null)
                return NotFound(movie);

            return StatusCode(200, movie);
        }

        [HttpPost]
        public IActionResult MovieAdd([FromBody] Movie movie)
        {
            // what is the best way to check for null 
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
            // confusions about what kind of linq request is best suit for this kind of example
            // what other way to make request 
           
            Movie? movie = _context.Movies.Where(m => m.Id == movieId).SingleOrDefault();

            // This logic I wrote bymyself .... I was advised to do research online and find out information from there...
            // I did not see anybody is using this implemention
            // Instead they use something different and it was a lot of information I did not understand which made me more confused becuse I don't have a fundumentals of API except that That API should have GET, POST, PUT,DELETE

            if (movie == null)
                return NotFound(movie);
            else
            {
                movie.Title = movieUpdt.Title;
                movie.Duration = movieUpdt.Duration;
                movie.Genre = movieUpdt.Genre;
            }
            _context.SaveChanges();
            // after searching online i saw diffrenet inplementation of using return types
            // like this one CreatedAtAction(nameof(GetPost), new { id = post.Id }, null);
            return StatusCode(200, movie);
        }

        // also it was alot of foreign return types like async task<actionresult<Movie>>>

        [HttpDelete("{id}")]
        public IActionResult MovieDelete(int movieId)
        {
            // which one to use single or default or single. firstordefault or first. or something else
            Movie? movie = _context.Movies.SingleOrDefault(m => m.Id == movieId);

            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return NoContent();
        }

        

    }
}
