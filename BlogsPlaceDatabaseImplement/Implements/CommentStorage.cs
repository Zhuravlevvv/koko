using BlogsLogic.BindingModels;
using BlogsLogic.Interfaces;
using BlogsLogic.ViewModels;
using BlogsPlaceDatabaseImplement;
using BlogsPlaceDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogsPlaceDatabaseImplement.Implements
{
    public class CommentStorage : ICommentStorage
    {
        private Comment CreateModel(CommentBindingModel model, Comment comment)
        {
            comment.Title = model.Title;
            comment.Text = model.Text;
            comment.DateCreate = model.DateCreate;
            comment.CommentAuthor = model.CommentAuthor;
            comment.BlogId = model.BlogId;
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
                DateCreate = comment.DateCreate,
                BlogId = comment.BlogId,
                BlogName = comment.Blog.BlogName
            };
        }

        public List<CommentViewModel> GetFullList()
        {
            using (var context = new DatabaseContext())
            {
                return context.Comments
                    .Include(rec => rec.Blog)
                    .Select(CreateModel)
                    .ToList();
            }
        }

        public List<CommentViewModel> GetFilteredList(CommentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new DatabaseContext())
            {
                return context.Comments
                    .Include(rec => rec.Blog)
                    .Where(rec => rec.Title == model.Title)
                    .Select(CreateModel)
                    .ToList();
            }
        }

        public CommentViewModel GetElement(CommentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new DatabaseContext())
            {
                var comment = context.Comments
                    .Include(rec => rec.Blog)
                    .FirstOrDefault(rec => rec.Title == model.Title || rec.Id == model.Id);

                return comment != null
                    ? CreateModel(comment)
                    : null;
            }
        }

        public void Insert(CommentBindingModel model)
        {
            using (var context = new DatabaseContext())
            {
                context.Comments.Add(CreateModel(model, new Comment()));
                context.SaveChanges();
            }
        }

        public void Update(CommentBindingModel model)
        {
            using (var context = new DatabaseContext())
            {
                var comment = context.Comments.FirstOrDefault(rec => rec.Id == model.Id);

                if (comment == null)
                {
                    throw new Exception("Комментарий не найден");
                }

                CreateModel(model, comment);
                context.SaveChanges();
            }
        }

        public void Delete(CommentBindingModel model)
        {
            using (var context = new DatabaseContext())
            {
                var comment = context.Comments.FirstOrDefault(rec => rec.Id == model.Id);

                if (comment == null)
                {
                    throw new Exception("Комментарий не найден");
                }

                context.Comments.Remove(comment);
                context.SaveChanges();
            }
        }
    }
}
