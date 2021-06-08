using BlogsLogic.BindingModels;
using BlogsLogic.Interfaces;
using BlogsLogic.ViewModels;
using BlogsPlaceDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogsPlaceDatabaseImplement.Implements
{
    public class BlogStorage : IBlogStorage
    {
        private Blog CreateModel(BlogBindingModel model, Blog blog)
        {
            blog.BlogName = model.BlogName;
            blog.BlogAuthor = model.BlogAuthor;
            blog.DateCreate = model.DateCreate;
            return blog;
        }

        private BlogViewModel CreateModel(Blog blog)
        {
            return new BlogViewModel
            {
                Id = blog.Id,
                BlogName = blog.BlogName,
                BlogAuthor = blog.BlogAuthor,
                DateCreate = blog.DateCreate
            };
        }

        public List<BlogViewModel> GetFullList()
        {
            using (var context = new DatabaseContext())
            {
                return context.Blogs
                    .Select(CreateModel)
                    .ToList();
            }
        }

        public List<BlogViewModel> GetFilteredList(BlogBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new DatabaseContext())
            {
                return context.Blogs
                    .Where(rec => model.DateFrom.HasValue && model.DateTo.HasValue &&
                        rec.DateCreate.Date >= model.DateFrom.Value.Date && rec.DateCreate.Date <= model.DateTo.Value.Date)
                    .Select(blog => new BlogViewModel
                    {
                        Id = blog.Id,
                        BlogName = blog.BlogName,
                        BlogAuthor = blog.BlogAuthor,
                        DateCreate = blog.DateCreate,
                        Comments = blog.Comments.Select(comment => new CommentViewModel
                        {
                            Title = comment.Title,
                            CommentAuthor = comment.CommentAuthor,
                            DateCreate = comment.DateCreate
                        }).ToList()
                    })
                    .ToList();
            }
        }

        public BlogViewModel GetElement(BlogBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new DatabaseContext())
            {
                var blog = context.Blogs.FirstOrDefault(rec => rec.BlogName == model.BlogName ||
                    rec.Id == model.Id);

                return blog != null
                    ? CreateModel(blog)
                    : null;
            }
        }

        public void Insert(BlogBindingModel model)
        {
            using (var context = new DatabaseContext())
            {
                context.Blogs.Add(CreateModel(model, new Blog()));
                context.SaveChanges();
            }
        }

        public void Update(BlogBindingModel model)
        {
            using (var context = new DatabaseContext())
            {
                var blog = context.Blogs.FirstOrDefault(rec => rec.Id == model.Id);

                if (blog == null)
                {
                    throw new Exception("Блог не найден");
                }

                CreateModel(model, blog);
                context.SaveChanges();
            }
        }

        public void Delete(BlogBindingModel model)
        {
            using (var context = new DatabaseContext())
            {
                var blog = context.Blogs.FirstOrDefault(rec => rec.Id == model.Id);

                if (blog == null)
                {
                    throw new Exception("Блог не найден");
                }

                context.Blogs.Remove(blog);
                context.SaveChanges();
            }
        }
    }
}
