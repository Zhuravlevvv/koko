using BlogsLogic.BindingModels;
using BlogsLogic.ViewModels;
using System.Collections.Generic;

namespace BlogsLogic.Interfaces
{
    public interface IBlogStorage
    {
        List<BlogViewModel> GetFullList();

        List<BlogViewModel> GetFilteredList(BlogBindingModel model);

        BlogViewModel GetElement(BlogBindingModel model);

        void Insert(BlogBindingModel model);

        void Update(BlogBindingModel model);

        void Delete(BlogBindingModel model);
    }
}