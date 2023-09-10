using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using bookstore.Models;
using bookstore.Models.Repositories;
using bookstore.ViewModels;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace bookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository<Book> bookRepository;
        private readonly IBookRepository<Author> authorRepository;

        private readonly IWebHostEnvironment hosting;

       

        public BookController(IBookRepository<Book> bookRepository,IBookRepository<Author> authorRepository
            ,IWebHostEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }
        // GET: /<controller>/
        public ActionResult Index()
        {
            var books = bookRepository.List();
            return View(books);
        }
        public ActionResult Details(int id)
        { var book = bookRepository.Find(id);
            return View(book);
        }

        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            { Author = FillSelectList() };
            return View(model);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {if (ModelState.IsValid)

            {
                try
                {
                    string FileName = uploadfile(model.File)?? string.Empty;
                   
                if (model.AuthorId == -1)
                {
                    ViewBag.Messege = "Please Select an Athor From the List!";
                    return View(FillAuthorsList());
                }
                
                    var author = authorRepository.Find(model.AuthorId);
                    Book book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = author,
                        ImgUrl =FileName
                    };
                    bookRepository.Add(book);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            ModelState.AddModelError(""," You have to fill all the required fields!");
            return View(FillAuthorsList());
        }

        // fill athors list
        BookAuthorViewModel FillAuthorsList()
        {
            var vmodel = new BookAuthorViewModel
            { Author = FillSelectList() };
            return (vmodel);
        }

        string uploadfile(IFormFile file,string imageurl)
        {
            if (file != null)
            {
                string Uploads = Path.Combine(hosting.WebRootPath, "Uploads");
                
                string FullPath = Path.Combine(Uploads, file.FileName);
                //Delete OLD file
                
                string FullOldPath = Path.Combine(Uploads, imageurl);
                if (FullPath != FullOldPath)
                {
                    System.IO.File.Delete(FullOldPath);
                    //Save NEW file
                    file.CopyTo(new FileStream(FullPath, FileMode.Create));
                }
                return file.FileName;
            }
            return imageurl;
        }

        string uploadfile(IFormFile file)
        {
            if (file != null)
            {
                string Uploads = Path.Combine(hosting.WebRootPath, "Uploads");
               
                string FullPath = Path.Combine(Uploads, file.FileName);
                file.CopyTo(new FileStream(FullPath, FileMode.Create));
                return file.FileName;

            }
            else
            return null;
        }


        public ActionResult Edit(int id)
        { var book = bookRepository.Find(id);
            var authorId = book.Author == null ? book.Author.Id=0: book.Author.Id;
            var viewmodel = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = authorId,
                Author = authorRepository.List().ToList(),
                ImageUrl = book.ImgUrl

            };
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel model)
        {
            try
            {
                
                    string FileName = uploadfile(model.File,model.ImageUrl);
                    
                    var author = authorRepository.Find(model.AuthorId);
                Book book = new Book
                {
                     Id = model.BookId,
                    Title = model.Title,
                    Description = model.Description,
                    Author = author,
                    ImgUrl = FileName
                    
                    
                };
                bookRepository.Update(model.BookId,book);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // file athors select on creat book bage
        List<Author> FillSelectList()
        { var athours = authorRepository.List().ToList();
            athours.Insert(0, new Author { Id = -1, FullName = "... Please Select Author ..." });
            return athours;
        }

        public ActionResult Delete(int id)
        { var book = bookRepository.Find(id);
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        { try
            {
                bookRepository.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult search(string term)
        { var result = bookRepository.Search(term);
            return View("Index", result);
        }
    }
    
}

