using BlogsLogic.BindingModels;
using BlogsLogic.Interfaces;
using BlogsLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace BlogsLogic.BusinessLogic
{
    public class CommentLogic
    {
        private readonly ICommentStorage commentStorage;

        public CommentLogic(ICommentStorage commentStorage)
        {
            this.commentStorage = commentStorage;
        }

        public List<CommentViewModel> Read(CommentBindingModel model)
        {
            if (model == null)
            {
                return commentStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<CommentViewModel> { commentStorage.GetElement(model) };
            }

            return commentStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(CommentBindingModel model)
        {
            var comment = commentStorage.GetElement(
                new CommentBindingModel
                {
                    Title = model.Title
                });

            if (comment != null && comment.Id != model.Id)
            {
                throw new Exception("Уже есть комментарий с таким заголовком");
            }

            if (model.Id.HasValue)
            {
                model.DateCreate = comment.DateCreate;
                commentStorage.Update(model);
            }
            else
            {
                model.DateCreate = DateTime.Now;
                commentStorage.Insert(model);
            }
        }

        public void Delete(CommentBindingModel model)
        {
            var comment = commentStorage.GetElement(
                new CommentBindingModel
                {
                    Id = model.Id
                });

            if (comment == null)
            {
                throw new Exception("Комментарий не найден");
            }

            commentStorage.Delete(model);
        }
    }
}