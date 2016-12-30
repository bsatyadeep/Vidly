using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        [Route("movies/index/{pageIndex=1}/{sortBy=Name}")]
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            //if (!pageIndex.HasValue)
            //{
            //    pageIndex = 1;
            //}
            //if (string.IsNullOrWhiteSpace(sortBy))
            //{
            //    sortBy = "Name";
            //}
            //var _movies = _context.Movies.Include(g => g.Genre).ToList();

            //return View(_movies);
            if (User.IsInRole(RoleName.CanManageMovie))
                return View("index");
            return View("ReadOnlyIndex");
        }

        [Route("movies/create")]
        [Authorize(Roles = RoleName.CanManageMovie)]
        public ActionResult Create()
        {
            var movie = new MovieViewModel
            {
                Movie = new Movie
                {
                    ReleaseDate = DateTime.Now,
                    NumberInStock = 0
                },
                Genres = _context.Genres.ToList()
            };
            return View(movie);
        }
        [Route("movies/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovie)]
        public ActionResult Create(MovieViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View(viewModel);
            }
            var movieExists = _context.Movies.FirstOrDefault(m => m.Name == viewModel.Movie.Name) != null;
            if (!movieExists)
            {
                Movie newMovie = new Movie
                {
                    Name = viewModel.Movie.Name,
                    DateAdded = viewModel.Movie.DateAdded,
                    ReleaseDate = viewModel.Movie.ReleaseDate,
                    NumberInStock = viewModel.Movie.NumberInStock,
                    GenreId = viewModel.Movie.GenreId
                };
                _context.Movies.Add(newMovie);
                _context.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("Name", "Movie Name already exits.");
                viewModel.Genres = _context.Genres.ToList();
                return View(viewModel);
            }
            return RedirectToAction("index");
        }
        [Authorize(Roles = RoleName.CanManageMovie)]
        public ActionResult Edit(int? id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            var editMovie = new MovieViewModel { Movie = movie, Genres = _context.Genres.ToList() };
            return View(editMovie);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovie)]
        public ActionResult Edit(MovieViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View(viewModel);
            }
            var updateMove = _context.Movies.Find(viewModel.Movie.Id);
            if (updateMove == null)
            {
                return HttpNotFound();
            }
            var movieExists = _context.Movies.FirstOrDefault(m => m.Name == viewModel.Movie.Name) != null;
            if (movieExists)
            {
                updateMove.GenreId = viewModel.Movie.GenreId;
                updateMove.DateAdded = viewModel.Movie.DateAdded;
                updateMove.ReleaseDate = viewModel.Movie.ReleaseDate;
                updateMove.NumberInStock = viewModel.Movie.NumberInStock;
                _context.Entry(updateMove).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("Name", "Movie does not exists.");
                viewModel.Genres = _context.Genres.ToList();
                return View(viewModel);
            }

            return RedirectToAction("index");
        }
        //[Route("movies/release/{year}/{month:regex(\\d{4}):range(1,12)}")]
        //public ActionResult ByReleaseDate(int year, int month)
        //{
        //    return Content(year + "/" + month);
        //}
    }
}