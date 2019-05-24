using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Models;
using Lab1.ViewModels;

namespace Lab1.Services
{
    public interface ICommentService
    {

        IEnumerable<CommentGetModel> GetAllComments(string filter);
    }

    public class CommentService : ICommentService
        {
            private IntroDbContext dbContext;

            public CommentService(IntroDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public IEnumerable<CommentGetModel> GetAllComments(string filter)
            {

                var filterComment = !string.IsNullOrEmpty(filter);

                var qry = GetCommentAndMovie().Where(com => !filterComment || com.Text.Contains(filter)
                );

                return qry.ToList();
            }
            private IQueryable<CommentGetModel> GetCommentAndMovie()
            {
                var commentAndMovie = from comment in dbContext.Comment
                    join movie in dbContext.Movies
                        on comment.MovieId equals movie.Id
                    select new CommentGetModel()
                    {
                        Id = comment.Id,
                        Text = comment.Text,
                        Important = comment.Important,
                        MovieId = comment.MovieId
                    };

                return dbContext.Comment.Select(comment => CommentGetModel.FromComment(comment));
            }
        }
    }

