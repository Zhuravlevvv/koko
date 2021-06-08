using BlogsPlaceDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogsPlaceDatabaseImplement
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlogsDB;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Blog> Blogs { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }
    }
}
