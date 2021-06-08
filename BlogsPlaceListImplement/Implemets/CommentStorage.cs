using BlogsLogic.BindingModels;
using BlogsLogic.Interfaces;
using BlogsLogic.ViewModels;
using BlogsPlaceListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogsPlaceListImplement.Implemets
{
    public class CommentStorage : ICommentStorage
    {
        private readonly ListDataSingleton source;

        public CommentStorage()
        {
            source = ListDataSingleton.GetInstance();
        }

        private Comment CreateModel(CommentBindingModel model, Comment comment)
        {
            comment.Title = model.Title;
            comment.Text = model.Text;
            comment.CommentAuthor = model.CommentAuthor;
            comment.BlogId = model.BlogId;
            comment.DateCreate = model.DateCreate;

            return comment;
        }

        private CommentViewModel CreateModel(Comment comment)
        {
            return new CommentViewModel
            {
                Id = comment.Id,
                Title = comment.Title,
                Text = comment.Text,
                CommentAuthor = comment.CommentAuthor,
                BlogId = comment.BlogId,
                BlogName = source.Blogs.FirstOrDefault(rec => rec.Id == comment.BlogId)?.BlogName,
                DateCreate = comment.DateCreate
            };
        }

        public List<CommentViewModel> GetFullList()
        {
            return source.Comments
                .Select(CreateModel)
                .ToList();
        }

        public List<CommentViewModel> GetFilteredList(CommentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            return source.Comments
                 .Where(rec => rec.Title == model.Title)
                .Select(CreateModel)
                .ToList();
        }

        public CommentViewModel GetElement(CommentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            var comment = source.Comments
                .FirstOrDefault(rec => rec.Title == model.Title || rec.Id == model.Id);

            return comment != null ? CreateModel(comment) : null;
        }

        public void Insert(CommentBindingModel model)
        {
            int maxId = source.Comments.Count > 0 ? source.Comments.Max(rec => rec.Id) : 0;
            var comment = new Comment { Id = maxId + 1 };
            source.Comments.Add(CreateModel(model, comment));
        }

        public void Update(CommentBindingModel model)
        {
            var comment = source.Comments.FirstOrDefault(rec => rec.Id == model.Id);

            if (comment == null)
            {
                throw new Exception("Комментарий не найден");
            }

            CreateModel(model, comment);
        }

        public void Delete(CommentBindingModel model)
        {
            var comment = source.Comments.FirstOrDefault(rec => rec.Id == model.Id);

            if (comment != null)
            {
                source.Comments.Remove(comment);
            }
            else
            {
                throw new Exception("Комментарий не найден");
            }
        }
    }
}
