using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace bookstore.Models
{
    public class BookStoreDbContext:DbContext

    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> Options):base(Options)
        {
        }
        public DbSet <Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
