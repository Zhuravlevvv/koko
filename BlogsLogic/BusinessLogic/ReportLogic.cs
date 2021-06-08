using BlogsLogic.BindingModels;
using BlogsLogic.HelperModels;
using BlogsLogic.Interfaces;
using BlogsLogic.ViewModels;
using System.Collections.Generic;

namespace BlogsLogic.BusinessLogic
{
    public class ReportLogic
    {
        private readonly IBlogStorage blogStorage;

        public ReportLogic(IBlogStorage blogStorage)
        {
            this.blogStorage = blogStorage;
        }

        private List<ReportBlogViewModel> GetBlogList(ReportBindingModel model)
        {
            var blogs = blogStorage.GetFilteredList(new BlogBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });

            var list = new List<ReportBlogViewModel>();

            foreach (var blog in blogs)
            {
                foreach (var comment in blog.Comments)
                {
                    list.Add(new ReportBlogViewModel
                    {
                        BlogName = blog.BlogName,
                        BlogDateCreate = blog.DateCreate,
                        CommentTitle = comment.Title,
                        CommentAuthor = comment.CommentAuthor,
                        CommentDateCreate = comment.DateCreate
                    });
                }
            }

            return list;
        }

        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список блогов",
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                Blogs = GetBlogList(model)
            });
        }
    }
}
