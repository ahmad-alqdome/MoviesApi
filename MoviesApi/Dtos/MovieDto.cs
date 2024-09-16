using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApi.Dtos
{
    public class MovieDto
    {
       
        [MaxLength(250)]
        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        public string Storeline { get; set; }

        public IFormFile? Poster { get; set; }

        [ForeignKey(nameof(GenreId))]
        public byte GenreId { get; set; }
    }
}
