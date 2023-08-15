using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo_Web_App.Models
{
    public class ToDoContext :DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {

        }

        public DbSet<ToDo> ToDos { get; set; } = null!;
        public DbSet<Category> categories { get; set; } = null!;
        public DbSet<Status> statuses { get; set; } = null!; 

        // seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
               new Category { categoryID = "work", categoryName = "Work" },
               new Category { categoryID = "home", categoryName = "Home" },
               new Category { categoryID = "shop", categoryName = "Shopping" },
               new Category { categoryID = "call", categoryName = "Contact" },
               new Category { categoryID = "ex", categoryName = "Exercise" }
            );

            modelBuilder.Entity<Status>().HasData(
                new Status { statusID = "open", statusName = "Open"},
                new Status { statusID = "closed", statusName = "Closed" }
            );
            
        }

    }
}
