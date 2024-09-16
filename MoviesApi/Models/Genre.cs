using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApi.Models
{
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte GenreId { get; set; }

        [Required ,MaxLength(100)]
        public string GenreName { get; set; }

        //public ICollection<Movie> Movies { get; set; }
    }
}
