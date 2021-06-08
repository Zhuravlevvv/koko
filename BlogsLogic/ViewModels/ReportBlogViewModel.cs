using System;

namespace BlogsLogic.ViewModels
{
    public class ReportBlogViewModel
    {
        public string BlogName { get; set; }

        public DateTime BlogDateCreate { get; set; }

        public string CommentTitle { get; set; }

        public string CommentAuthor { get; set; }

        public DateTime CommentDateCreate { get; set; }
    }
}
