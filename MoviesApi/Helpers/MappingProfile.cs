using AutoMapper;

namespace MoviesApi.Helpers
{
    public class MappingProfile : Profile
    {

        public MappingProfile() { 
        
             CreateMap<Movie, MoviesDetailsDto>(); // when you have a move will chang it to Movie Details Dto  
            CreateMap<MovieDto, Movie>()
               .ForMember(src => src.Poster, opt => opt.Ignore());


        }
    }
}
