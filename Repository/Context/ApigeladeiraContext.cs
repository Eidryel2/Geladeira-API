using Microsoft.EntityFrameworkCore;
using Domain;

namespace Repository.Models
{
    public  class ApiGeladeiraContext : DbContext
    {
        public DbSet<ItemDomain> Items { get; set; }
        public ApiGeladeiraContext(DbContextOptions<ApiGeladeiraContext> options)
            : base(options)
        {
        }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemDomain>().HasKey(e => e.Id);
            modelBuilder.Entity<ItemDomain>().Property(e => e.Nome).IsRequired();
               

               
            
        }
    }
}
