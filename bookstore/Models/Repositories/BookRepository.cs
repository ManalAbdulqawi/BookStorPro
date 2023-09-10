using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Models.Repositories
{
    public class BookRepository:IBookRepository<Book>
    {
        List<Book> books;
        

        public BookRepository()
        {
            books = new List<Book>()
        { new Book
        {Id=1, Title="c++ programing", Description="no description", Author=new Author (),ImgUrl="tt.jpg"

        },

        new Book
        {Id=2, Title="c programing", Description="no thing",Author=new Author (),ImgUrl="hh.jpg"

        },

        new Book
        {Id=3, Title="java programing", Description="no text",Author=new Author (),ImgUrl="ss.jpg"

        }
        };
        }

        public void Add(Book entity)
        {
            entity.Id = books.Max(b=>b.Id)+1;
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = Find(id);//books.SingleOrDefault(b=> b.Id==id);
            books.Remove(book);
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b =>b.Id ==id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public List<Book> Search(string term)
        {
            var result = books.Where(b => b.Title.Contains(term)
                   || b.Description.Contains(term)
                   || b.Author.FullName.Contains(term)).ToList();
            return result;

        }

        public void Update(int id,Book newbook)
        {
            var book = Find(id);//books.SingleOrDefault(b => b.Id == id);
            book.Title = newbook.Title;
            book.Author= newbook.Author;
            book.Description = newbook.Description;
            book.ImgUrl = newbook.ImgUrl;


        }
    }
}
