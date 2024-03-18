using CoreAPISeedData_1Tekrar.Extensions;
using CoreAPISeedData_1Tekrar.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreAPISeedData_1Tekrar.Models.ContextClasses
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

        public DbSet<Category> Categories { get; set; }
    }
}
