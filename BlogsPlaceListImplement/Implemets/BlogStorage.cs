using BlogsLogic.BindingModels;
using BlogsLogic.Interfaces;
using BlogsLogic.ViewModels;
using BlogsPlaceListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogsPlaceListImplement.Implemets
{
    public class BlogStorage : IBlogStorage
    {
        private readonly ListDataSingleton source;

        public BlogStorage()
        {
            source = ListDataSingleton.GetInstance();
        }

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
            return source.Blogs
                .Select(CreateModel)
                .ToList();
        }

        public List<BlogViewModel> GetFilteredList(BlogBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            return source.Blogs
                .Where(rec => model.DateFrom.HasValue && model.DateTo.HasValue &&
                        rec.DateCreate.Date >= model.DateFrom.Value.Date && rec.DateCreate.Date <= model.DateTo.Value.Date)
                .Select(CreateModel)
                .ToList();
        }

        public BlogViewModel GetElement(BlogBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            var blog = source.Blogs
                .FirstOrDefault(rec => rec.BlogName == model.BlogName || rec.Id == model.Id);

            return blog != null ? CreateModel(blog) : null;
        }

        public void Insert(BlogBindingModel model)
        {
            int maxId = source.Blogs.Count > 0 ? source.Blogs.Max(rec => rec.Id) : 0;
            var blog = new Blog { Id = maxId + 1 };
            source.Blogs.Add(CreateModel(model, blog));
        }

        public void Update(BlogBindingModel model)
        {
            var blog = source.Blogs.FirstOrDefault(rec => rec.Id == model.Id);

            if (blog == null)
            {
                throw new Exception("Блог не найден");
            }

            CreateModel(model, blog);
        }

        public void Delete(BlogBindingModel model)
        {
            var blog = source.Blogs.FirstOrDefault(rec => rec.Id == model.Id);

            if (blog != null)
            {
                source.Blogs.Remove(blog);
            }
            else
            {
                throw new Exception("Блог не найден");
            }
        }
    }
}
