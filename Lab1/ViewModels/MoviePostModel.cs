using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Models;

namespace Lab1.ViewModels
{
    public class MoviePostModel
    {
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Movie title must be at least 2 characters!")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string MovieGenre { get; set; }
        public int DurationInMinutes { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public DateTime DateAdded { get; set; }
        [Range(1, 10)]
        public double Rating { get; set; }
        public string WasWatched { get; set; }
        public List<Comment> Comments { get; set; }


        public static Movie ToMovie(MoviePostModel movie)
        {
            Genre movieGenre = Genre.Action;

            if (movie.MovieGenre == "Comedy")
            {
                movieGenre = Genre.Comedy;
            }
            else if (movie.MovieGenre == "Horror")
            {
                movieGenre = Genre.Horror;
            }
            else if (movie.MovieGenre == "Crime")
            {
                movieGenre = Genre.Crime;
            }

            Watched watched = Watched.No;

            if (movie.WasWatched == "YES")
            {
                watched = Watched.Yes;
            }

            return new Movie
            {
                Title = movie.Title,
                Description = movie.Description,
                MovieGenre = movieGenre,
                DurationInMinutes = movie.DurationInMinutes,
                ReleaseYear = movie.ReleaseYear,
                Director = movie.Director,
                DateAdded = movie.DateAdded,
                Rating = movie.Rating,
                WasWatched = watched,
                Comment = movie.Comments
            };
        }
    }
}
