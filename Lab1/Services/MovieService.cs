using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Models;
using Lab1.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Services
{
    public interface IMovieService
    {
        IEnumerable<MovieGetModel> GetAllMovies(DateTime? from = null, DateTime? to = null);
        Movie GetById(int id);
        Movie Create(MoviePostModel movie);
        Movie Update(int id, Movie movie);
        Movie Delete(int id);
        void DeleteAll();
    }
    public class MovieService : IMovieService
    {
        public IntroDbContext DbContext;

        public MovieService(IntroDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        public IEnumerable<MovieGetModel> GetAllMovies(DateTime? from = null, DateTime? to = null)
        {
            IQueryable<Movie> result = DbContext.Movies.Include(f => f.Comment).OrderByDescending(f => f.ReleaseYear);

            if (from == null && to == null)
            {
                return result.Select(movie => MovieGetModel.FromMovie(movie));
            }
            if (from != null)
            {
                result = result.Where(f => f.DateAdded > from);
            }
            if (to != null)
            {
                result = result.Where(f => f.DateAdded < to);
            }

            foreach (var entity in result.Select(movie => MovieGetModel.FromMovie(movie)))
            {
                Console.WriteLine(entity.ToString());
            }
            return result.Select(movie => MovieGetModel.FromMovie(movie));
        }

        public Movie GetById(int id)
        {
            return DbContext.Movies.Include(c => c.Comment).FirstOrDefault(m => m.Id == id);
        }

        public Movie Create(MoviePostModel movieDTO)
        {
            Movie addMovie = MoviePostModel.ToMovie(movieDTO);
            DbContext.Movies.Add(addMovie);
            DbContext.SaveChanges();
            return addMovie;
        }

        public Movie Update(int id, Movie movie)
        {
            var existing = DbContext.Movies.AsNoTracking().FirstOrDefault(c => c.Id == id);

            if (existing == null)
            {
                DbContext.Movies.Add(movie);
                DbContext.SaveChanges();
                return movie;
            }
            movie.Id = id;
            DbContext.Movies.Update(movie);
            DbContext.SaveChanges();

            return movie;
        }

        public Movie Delete(int id)
        {
            Movie movie = DbContext.Movies.Include(c => c.Comment).FirstOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return null;
            }
            DbContext.Remove(movie);
            DbContext.SaveChanges();

            return movie;
        }

        public void DeleteAll()
        {
            foreach (var entity in DbContext.Movies)
            {
                DbContext.Movies.Remove(entity);
            }

            DbContext.SaveChanges();
        }
    }
}
