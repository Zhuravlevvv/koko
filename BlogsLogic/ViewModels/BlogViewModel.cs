using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BlogsLogic.ViewModels
{
    public class BlogViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string BlogName { get; set; }

        [DisplayName("ФИО автора")]
        public string BlogAuthor { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        public List<CommentViewModel> Comments { get; set; }
    }
}