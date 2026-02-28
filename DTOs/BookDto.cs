using BookstoreManager.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BookstoreManager.Enums;

namespace BookstoreManager.Dto
{
    public class BookDto
    {
        [Required(ErrorMessage = "O campo Título é obrigatório!")]
        [StringLength(120, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 120 caracteres!")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "O campo Autor é obrigatório!")]
        [StringLength(120, MinimumLength = 2, ErrorMessage = "O autor deve ter entre 2 e 120 caracteres!")]
        public string? Author { get; set; }

        [Required(ErrorMessage = "O campo Gênero é obrigatório!")]
        public string? Genre { get; set; }
        

        [Required(ErrorMessage = "O campo Preço é obrigatório!")]
        [Range(1, double.MaxValue, ErrorMessage = "O preço não pode ser negativo!")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "O campo Estoque é obrigatório!")]
        [Range(1, double.MaxValue, ErrorMessage = "O estoque não pode ser negativo!")]
        public int Stock { get; set; }
    }
}
