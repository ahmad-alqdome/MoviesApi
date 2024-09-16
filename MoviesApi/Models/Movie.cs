using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApi.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        public string Storeline {  get; set; }  

        public byte[] Poster { get; set; }

        [ForeignKey(nameof(GenreId))]
        public byte GenreId {  get; set; }  
        public Genre Genre { get; set; }
    

    }

}
