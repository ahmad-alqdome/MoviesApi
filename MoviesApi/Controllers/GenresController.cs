using Microsoft.AspNetCore.Mvc;
using MoviesApi.DTOs;
using MoviesApi.Services;


namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {

        private readonly IGenresService _genreService;
     
        public GenresController(IGenresService genresService)
        {
                _genreService = genresService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _genreService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult>AddGenreAsync([FromBody]AddGenreDto genre)
        {
            var newGenre = new Genre { GenreName = genre.GenreName };
            await _genreService.Add(newGenre);
            return Ok(newGenre);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenreAsync(byte id,[FromBody] AddGenreDto newGenre)
        {
            var genre =await  _genreService.GetById(id);

            if (genre == null)
            {
                return NotFound($"No genre was found with Id : {id}");
            }

            genre.GenreName = newGenre.GenreName;
            _genreService.Update(genre);
            return  Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveGenre(byte id)
        {
            var genre =await _genreService.GetById(id);

            if (genre == null)
                return NotFound("No Found");
          
            _genreService.Delete(genre);

            return Ok(genre);
        }

    }
}
