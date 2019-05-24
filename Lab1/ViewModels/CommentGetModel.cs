using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Models;

namespace Lab1.ViewModels
{
    public class CommentGetModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Important { get; set; }
        public int MovieId { get; set; }

        public static CommentGetModel FromComment(Comment comment)
        {
            return new CommentGetModel
            {
                Id = comment.Id,
                Text = comment.Text,
                Important = comment.Important,
                MovieId = comment.MovieId
            };
        }
    }
}
