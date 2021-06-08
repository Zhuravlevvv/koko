using System;

namespace BlogsPlaceListImplement.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime DateCreate { get; set; }

        public string CommentAuthor { get; set; }

        public int BlogId { get; set; }
    }
}