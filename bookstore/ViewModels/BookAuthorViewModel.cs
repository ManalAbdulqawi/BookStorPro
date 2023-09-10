using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using bookstore.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace bookstore.ViewModels
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }

        [Required]
        [StringLength(30,MinimumLength =5)]
        public string Title { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 5)]
        public string Description { get; set; }

        public string ImageUrl
        { get; set; }

        public int AuthorId { get; set; }

        public List<Author> Author { get; set; }

        public IFormFile File
        { get; set; }
    }
}
