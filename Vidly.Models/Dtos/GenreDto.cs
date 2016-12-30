using System.ComponentModel.DataAnnotations;

namespace Vidly.Models.Dtos
{
    public class GenreDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
