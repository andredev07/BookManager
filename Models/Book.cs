using BookstoreManager.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookstoreManager.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public BookGenre Genre { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public Book()
        {
            Id = Guid.NewGuid();
        }
    }
}
