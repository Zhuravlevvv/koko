using BlogsLogic.BindingModels;
using BlogsLogic.Interfaces;
using BlogsLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogsLogic.BusinessLogic
{
    public class BlogLogic
    {
        private readonly IBlogStorage blogStorage;

        public BlogLogic(IBlogStorage blogStorage)
        {
            this.blogStorage = blogStorage;
        }

        public List<BlogViewModel> Read(BlogBindingModel model)
        {
            if (model == null)
            {
                return blogStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<BlogViewModel> { blogStorage.GetElement(model) };
            }

            return blogStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(BlogBindingModel model)
        {
            var blog = blogStorage.GetElement(
                new BlogBindingModel
                {
                    BlogName = model.BlogName
                });

            if (blog != null && blog.Id != model.Id)
            {
                throw new Exception("Уже есть блог с таким названием");
            }

            if (model.Id.HasValue)
            {
                model.DateCreate = blog.DateCreate;
                blogStorage.Update(model);
            }
            else
            {
                model.DateCreate = DateTime.Now;
                blogStorage.Insert(model);
            }
        }

        public void Delete(BlogBindingModel model)
        {
            var blog = blogStorage.GetElement(
                new BlogBindingModel
                {
                    Id = model.Id
                });

            if (blog == null)
            {
                throw new Exception("Блог не найден");
            }

            blogStorage.Delete(model);
        }
    }
}
