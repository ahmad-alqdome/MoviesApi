
using MoviesApi.Dtos;
using MoviesApi.Models;

namespace MoviesApi.Services
{
    public class MoviesService : IMoviesService
    {

        private readonly ApplicationDbContext _context;

        public MoviesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAll(byte id = 0)
        {
         return await _context.Movies
                .OrderByDescending(x => x.Rate)
                .Where(x => x.GenreId == id||id==0)
                .Include(m => m.Genre)
                .ToListAsync();
        }

        public async Task<Movie> GetById(byte id)
        {
            return await _context.Movies.Include(m => m.Genre).FirstOrDefaultAsync(m => m.Id == id); 
        }

        //public async  Task<IEnumerable<Movie>> GetMoviesByGenreId(byte id)
        //{
        //  throw new NotImplementedException();  
        //}

        public async  Task<Movie> Add(Movie movie)
        {
           
            await _context.AddAsync(movie);
            _context.SaveChanges();

            return movie;

        }
        public Movie Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();

            return movie;
        }

        public Movie Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return movie;
        }

    }
}
