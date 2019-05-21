using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Models
{
    public class DataSeeder
    {
        public static void Initialize(IntroDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Movies.Any())
            {
                return;   // DB has been seeded
            }

            context.Movies.AddRange(
                new Movie
                {
                    Title = "Avengers",
                    Runtime = 2,
                    Rating = 5,
                    StoryLine = "A team of hearos save the earth!"
                },
                new Movie
                {
                    Title = "Captain Marvel",
                    Runtime = 3,
                    Rating = 5,
                    StoryLine = "The boss of the Marvel heroes!"
                });
            context.SaveChanges();
        }

    }
}
