using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Vidly.Models;
using Vidly.Models.Dtos;
namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize]
        public IHttpActionResult GetMovies()
        {
            var movies = _context.Movies
                .Include(m => m.Genre).ToList()
                .Select(Mapper.Map<Movie, MovieDto>);

            return Ok(movies);
        }
        [Authorize]
        public IHttpActionResult GetMovies(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovie)]
        public IHttpActionResult CreateMovie(MovieDto movie)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var newMovie = Mapper.Map<MovieDto, Movie>(movie);
            _context.Movies.Add(newMovie);
            return Ok(movie);
        }
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovie)]
        public IHttpActionResult UpdateMovie(int id, MovieDto movie)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var updateMovie = _context.Movies.Find(id);
            if (updateMovie == null)
                return BadRequest();
            Mapper.Map<MovieDto, Movie>(movie, updateMovie);

            _context.Entry(updateMovie).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(movie);
        }
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovie)]
        public void DeleteMovie(int id)
        {
            var deleteMovie = _context.Movies.Find(id);
            if (deleteMovie != null)
                _context.Movies.Remove(deleteMovie);
        }
    }
}
