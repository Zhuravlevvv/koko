using BlogsLogic.BusinessLogic;
using BlogsLogic.Interfaces;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;
using BlogsPlaceListImplement.Implemets;

namespace BlogsPlaceView
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var container = BuildUnityContainer();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();

            currentContainer.RegisterType<BlogLogic>(new
                HierarchicalLifetimeManager());
            currentContainer.RegisterType<CommentLogic>(new
                HierarchicalLifetimeManager());
            currentContainer.RegisterType<ReportLogic>(new
                HierarchicalLifetimeManager());

            currentContainer.RegisterType<IBlogStorage, BlogStorage>(new
                HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICommentStorage, CommentStorage>(new
                HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}