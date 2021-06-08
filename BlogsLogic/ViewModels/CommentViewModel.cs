using System;
using System.ComponentModel;

namespace BlogsLogic.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Текст")]
        public string Text { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [DisplayName("ФИО автора")]
        public string CommentAuthor { get; set; }

        public int BlogId { get; set; }

        [DisplayName("Название блога")]
        public string BlogName { get; set; }
    }
}