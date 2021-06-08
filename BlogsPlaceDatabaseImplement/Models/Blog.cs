using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogsPlaceDatabaseImplement.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string BlogName { get; set; }

        public string BlogAuthor { get; set; }

        public DateTime DateCreate { get; set; }

        [ForeignKey("BlogId")]
        public virtual List<Comment> Comments { get; set; }
    }
}
