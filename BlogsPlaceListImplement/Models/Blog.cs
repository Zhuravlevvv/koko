using System;

namespace BlogsPlaceListImplement.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string BlogName { get; set; }

        public string BlogAuthor { get; set; }

        public DateTime DateCreate { get; set; }
    }
}
