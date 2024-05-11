using AutoMapper;
using BookSrote.API.Data;
using BookSrote.API.Models;

namespace BookSrote.API.Helpers
{
    public class ApplicationMapper: Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Books, BookModel>().ReverseMap();
        }
    }
}
