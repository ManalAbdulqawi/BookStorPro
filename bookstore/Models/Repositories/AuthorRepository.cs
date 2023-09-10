using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace bookstore.Models.Repositories
{
    public class AuthorRepository: IBookRepository<Author>
    {
        IList<Author> authors;
        

        public AuthorRepository()
        {
            authors = new List<Author>()
            {new Author {Id =1,FullName ="Manal Mohsin" },
            new Author {Id =2,FullName ="Mustafa Saleh" },
              new Author {Id =3,FullName ="Saleh Abdulkawi" }

            };
        }

        public void Add(Author entity)
        {
           entity.Id= authors.Max(b => b.Id) + 1;
            authors.Add(entity);
        }

        public void Delete(int id)
        {
            var author = Find(id);
            authors.Remove(author );
        }

        public Author Find(int id)
        {
            var author = authors.SingleOrDefault(a=>a.Id ==id);
            return author;
        }

        public IList<Author> List()
        {
            return authors;
        }

        public List<Author> Search(string term)
        {
            return authors.Where(a => a.FullName.Contains(term)).ToList();
        }

        public void Update(int id, Author newauthor)
        {
            var author = Find(id);
            author.FullName = newauthor.FullName;
            
        }
    }
}
