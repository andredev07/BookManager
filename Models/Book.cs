using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookstoreManager.Models
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(120, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 120 caracteres!")]
        public string Title { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 120 caracteres!")]
        public string Author { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BookGenre Genre { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "O preço não pode ser negativo!")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "O estoque não pode ser negativo!")]
        public int Stock { get; set; }

        public Book()
        {
            Id = Guid.NewGuid();
        }

    }

    public enum BookGenre
    {
        Ficcao,
        Romance,
        Misterio,
        Fantasia,
        Terror
    }
}
