using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class ApplicationDbContext : DbContext //inherits from the DBContext class which is in-built in the entityframeworkcore.
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; } //Defines the entity which must be a class. Property is the name of the table (Categories). This will create a table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(//We want to create in the category these new entities/records
                new Category { CategoryId = 1, Name = "Action" , DisplayOrder= 1 },
                new Category { CategoryId = 2, Name = "Horror", DisplayOrder = 2 },
                new Category { CategoryId = 3, Name = "Drama", DisplayOrder = 3 }
                );
        }
    }
}

//commands
//update-database ////Updates database if there are any migrations applied to database. Checks the migrations folder for the latest migration
//add-migration ///////Create command to create table
//remove-migration //////to remove the migration command