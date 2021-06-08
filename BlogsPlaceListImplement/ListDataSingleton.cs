using BlogsPlaceListImplement.Models;
using System.Collections.Generic;

namespace BlogsPlaceListImplement
{
    public class ListDataSingleton
    {
        private static ListDataSingleton instance;

        public List<Blog> Blogs { get; set; }

        public List<Comment> Comments { get; set; }

        private ListDataSingleton()
        {
            Blogs = new List<Blog>();
            Comments = new List<Comment>();
        }

        public static ListDataSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new ListDataSingleton();
            }

            return instance;
        }
    }
}