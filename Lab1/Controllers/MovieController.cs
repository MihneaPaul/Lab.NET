using System.Collections.Generic;
using System.Linq;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {

        private IntroDbContext context;

        public MovieController(IntroDbContext context)
        {
            this.context = context;
        }

        // GET: api/Movie
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return context.Movies;
        }

        // GET: api/Movie/5
        [HttpGet("{id}", Name = "Get")]
        public Movie Get(int id)
        {
            return context.Movies.FirstOrDefault(c => c.Id == id);
        }

        // POST: api/Movie
        [HttpPost]
        public IActionResult Post([FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Movies.Add(movie);
            context.SaveChanges();
            return Ok();
        }

        // PUT: api/Movie/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existing = context.Movies.FirstOrDefault(c => c.Id == id);
            if (existing != null)
            {
                movie.Id = existing.Id;
                context.Movies.Remove(existing);
            }
            context.Movies.Add(movie);
            context.SaveChanges();
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var found = context.Movies.FirstOrDefault(c => c.Id == id);
            if (found == null)
            {
                return NotFound();
            }
            context.Movies.Remove(found);
            context.SaveChanges();
            return Ok();
        }
    }
}
