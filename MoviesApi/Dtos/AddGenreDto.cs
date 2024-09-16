namespace MoviesApi.DTOs
{
    public class AddGenreDto
    {
        [MaxLength(100)]
        public string GenreName { get; set; }
    }
}
