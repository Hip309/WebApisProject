using Microsoft.EntityFrameworkCore;

namespace StoreApi.Entities;

public class StoreContext: DbContext
{
    public StoreContext (DbContextOptions<StoreContext> opt) : base(opt)
    {

    }

    #region DbSet
    public DbSet <User> Users { get; set; }
    public DbSet <Category> Categories { get; set; }
    public DbSet <Product> Products { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(e => { 
            e.ToTable("Category");
            e.HasKey(c => c.Id);
        });

        modelBuilder.Entity<Product>(e =>
        {
            e.ToTable("Product");
            e.HasKey(c => c.Id);
            
        });

    }
}
