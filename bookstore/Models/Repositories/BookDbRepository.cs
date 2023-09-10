using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Models.Repositories
{
    public class BookDbRepository : IBookRepository<Book>
    {
        BookStoreDbContext db;


        public BookDbRepository(BookStoreDbContext _db)
        {
            db = _db;
       
        }

        public List<Book> Search(string term)
        {
            var result = db.Books.Include(a => a.Author).Where(b => b.Title.Contains(term)
                   || b.Description.Contains(term)
                   || b.Author.FullName.Contains(term)).ToList();
            return result;

        }

        public void Add(Book entity)
        {
            
            db.Books.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Find(id);//books.SingleOrDefault(b=> b.Id==id);
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public Book Find(int id)
        {
            var book = db.Books.Include(a => a.Author).SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return db.Books.Include(a=>a.Author).ToList();
        }

        public void Update(int id, Book newbook)
        {
            db.Update(newbook);
            db.SaveChanges();

        }
    }
}
