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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=Sanad_HpEnvy\\SQLEXPRESS;database=Contacts;integrated security=true;TrustServerCertificate=true");
        }
    }
}
