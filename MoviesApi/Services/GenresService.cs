
using MoviesApi.Dtos;

namespace MoviesApi.Services
{
    public class GenresService : IGenresService
    {
        private readonly ApplicationDbContext _context;

        public GenresService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Genre>> GetAll(){
            return await _context.Genres.OrderBy(g=>g.GenreName).ToListAsync(); 
        }
        public async Task<Genre> GetById(byte id)
        {
            return  await _context.Genres.SingleOrDefaultAsync(g=>g.GenreId==id);
        }

        public async Task<Genre> Add(Genre genre)
        {
            await _context.AddAsync(genre);
            _context.SaveChanges();
            
            return genre;
        }


        public  Genre Update(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();

            return genre;
            
        }
        public Genre Delete(Genre genre)
        {
            _context.Remove(genre);
            _context.SaveChanges();

            return genre;
        }

        public async Task<bool> IsValidGenre(byte id)
        {
            return await _context.Genres.AnyAsync(g => g.GenreId == id);
        }
    }
}
