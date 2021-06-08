using System;

namespace BlogsLogic.BindingModels
{
    public class BlogBindingModel
    {
        public int? Id { get; set; }

        public string BlogName { get; set; }

        public string BlogAuthor { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateTo { get; set; }

        public DateTime? DateFrom { get; set; }
    }
}