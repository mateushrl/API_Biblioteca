using System.ComponentModel.DataAnnotations;

namespace AplicacaoBiblioteca.Models.DTO
{
    public class LivroDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        [Required]
        public string Autor { get; set; }
    }
}
