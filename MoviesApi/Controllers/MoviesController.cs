﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Dtos;
using MoviesApi.Models;
using MoviesApi.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        private readonly IGenresService _genresService;

        private List<string> allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;   //1 MB


        public MoviesController(IMoviesService moviesService, IGenresService genresService)
        {
            _moviesService = moviesService;
            _genresService = genresService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _moviesService.GetAll()) ;
            //TODO : map movies to DTO
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieByIdAsync(byte id)
        {
            var movie = await _moviesService.GetById(id);

            var movieDto = new MoviesDetailsDto {
                Id = movie.Id,
                GenreId = movie.GenreId,
                GenreName = movie.Genre?.GenreName,
                Title = movie.Title,
                Rate = movie.Rate,
                Storeline = movie.Storeline,
                Poster = movie.Poster,
                Year = movie.Year
            };

            return movie == null ? NotFound() : Ok(movieDto);

        }
        [HttpGet("GenreId")]
        public async Task<IActionResult> GetMovieByGenreIdAsync(byte Genreid)
        {

            return Ok(await _moviesService.GetAll(Genreid));
        }




        [HttpPost]
        public async Task<IActionResult> AddMovies([FromForm] MovieDto movieDto) 
        {
            if (movieDto.Poster == null)
            {
                return BadRequest("The Poster is requerd ");
            }
            if (!allowedExtenstions.Contains(Path.GetExtension(movieDto.Poster.FileName).ToLower()))
            {
                return BadRequest("Only .png or .jpg file ");
            }

            if (movieDto.Poster.Length > _maxAllowedPosterSize)
            {
                return BadRequest("The Size is higher ");
            }

            var isValidGenre = await _genresService.IsValidGenre(movieDto.GenreId);

            if (!isValidGenre)
            {
                return BadRequest("The Genre Id is Not Valid");
            }

            using var dataStream = new MemoryStream();

            await movieDto.Poster.CopyToAsync(dataStream);

            var movie = new Movie
            {
                Title = movieDto.Title,
                Year = movieDto.Year,
                GenreId = movieDto.GenreId,
                Poster = dataStream.ToArray(),
                Storeline = movieDto.Storeline,
                Rate = movieDto.Rate,
            };
           await _moviesService.Add(movie);

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveMovieAsync (byte  id)
        {
            var movie = await _moviesService.GetById(id);

            if (movie == null)

            return NotFound($"No Movie was found with ID {id}");
           

                _moviesService.Delete(movie);
                
             return    Ok(movie);
            
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovieAsync(byte id , [FromForm] MovieDto newMovie)
        {
            var movie = await _moviesService.GetById (id);

            if (movie == null)

                return NotFound($"No Movie was found with ID {id}"); 
            
            
                var isValidGenre = await _genresService.IsValidGenre(id);

                if (!isValidGenre)
                {
                    return BadRequest("The Genre Id is Not Valid");
                }


            if (newMovie.Poster != null)
            {
                if (!allowedExtenstions.Contains(Path.GetExtension(newMovie.Poster.FileName).ToLower()))
                {
                    return BadRequest("Only .png or .jpg file ");
                }

                if (newMovie.Poster.Length > _maxAllowedPosterSize)
                {
                    return BadRequest("The Size is higher ");

                }

                using var dataStream = new MemoryStream();

                await newMovie.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();
            }




                 movie.GenreId = newMovie.GenreId;
                 movie.Title = newMovie.Title;
                 movie.Rate = newMovie.Rate;
                 movie.Storeline = newMovie.Storeline;
                 movie.Year = newMovie.Year;

                _moviesService.Update(movie);
                
                return Ok(movie);
            
        }


    }
}
