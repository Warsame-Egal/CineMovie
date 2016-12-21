using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab4.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Lab4.Controllers
{
    public class Home : Controller
    {
        private MoviesContext _movieContext;

        /// <param name="context"></param>
        public Home(MoviesContext context)
        {
            _movieContext = context;
        }

        // Displays list of movies
        public IActionResult Index()
        {
            //return View();
            return View(_movieContext.Movies.ToList());
        }

        public IActionResult AddMovie()
        {
            return View();
        }

        public IActionResult CreateMovie(Movie movie)
        {
            _movieContext.Movies.Add(movie);
            _movieContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var movieToUpdate = (from c in _movieContext.Movies where c.MovieId == id select c).FirstOrDefault();

            return View(movieToUpdate);
        }

        public IActionResult ModifyMovie(Movie movie)
        {
            var id = Convert.ToInt32(Request.Form["MovieId"]);

            var movieToUpdate = (from c in _movieContext.Movies where c.MovieId == id select c).FirstOrDefault();
            movieToUpdate.Title = movie.Title;
            movieToUpdate.SubTitle = movie.SubTitle;
            movieToUpdate.Description = movie.Description;
            movieToUpdate.Year = movie.Year;
            movieToUpdate.Rating = movie.Rating;
            _movieContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteMovie(int id)
        {
            var movieToUpdate = (from c in _movieContext.Movies where c.MovieId == id select c).FirstOrDefault();
            _movieContext.Remove(movieToUpdate); //delete the movie object
            _movieContext.SaveChanges(); //save the changes
            return RedirectToAction("Index");
        }
    }
}
