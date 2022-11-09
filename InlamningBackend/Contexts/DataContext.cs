using InlamningBackend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InlamningBackend.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<StatusEntity> Status { get; set; }
        public DbSet<IssueEntity> Issues { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }

    }
}
