using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using library_app_bakend.Entites;
using Microsoft.EntityFrameworkCore;

namespace library_app_bakend.DbContextes
{
    public class LibraryDB : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Borrow> Borrws { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(@"Data source=DBFiles\librarydb.sqlite");
        }
    }
}