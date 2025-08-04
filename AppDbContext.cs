using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact
{
    internal class AppDbContext: DbContext
    {
        public DbSet<MyContacts> MyContacts { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=Sanad_HpEnvy\\SQLEXPRESS;database=Contacts;integrated security=true;TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>()
                .HasMany(c => c.ConList) //a category has many todoitems
                .WithOne(cl => cl.Category) //a todoitem has one category
                .HasForeignKey(cl => cl.CategoryId); //the foreign key is in todoitem
        }
    }
}
