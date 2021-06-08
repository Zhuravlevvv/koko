using BlogsLogic.BindingModels;
using BlogsLogic.ViewModels;
using System.Collections.Generic;

namespace BlogsLogic.Interfaces
{
    public interface ICommentStorage
    {
        List<CommentViewModel> GetFullList();

        List<CommentViewModel> GetFilteredList(CommentBindingModel model);

        CommentViewModel GetElement(CommentBindingModel model);

        void Insert(CommentBindingModel model);

        void Update(CommentBindingModel model);

        void Delete(CommentBindingModel model);
    }
}