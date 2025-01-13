namespace WebApi.Model
{
    using Microsoft.EntityFrameworkCore;
    using WebApi.Database;
    using WebApi.Entity;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(a => a.IdUser);  // Imposta 'IdUser' come chiave primaria
        }
    }

}
