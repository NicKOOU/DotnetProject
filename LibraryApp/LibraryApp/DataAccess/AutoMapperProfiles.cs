using AutoMapper;

namespace LibraryApp.DataAccess
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {
            CreateMap<EfModels.TBook, Dbo.Book>();
            CreateMap<Dbo.Book, EfModels.TBook>();

            CreateMap<EfModels.TComicsBook, Dbo.ComicsBook>();
            CreateMap<Dbo.ComicsBook, EfModels.TComicsBook>();

            CreateMap<EfModels.TMovie, Dbo.Movie>();
            CreateMap<Dbo.Movie, EfModels.TMovie>();
        }
    }
}
