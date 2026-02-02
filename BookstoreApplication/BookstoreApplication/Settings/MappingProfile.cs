using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookstoreApplication.Settings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDTO>()
                .ForMember(
                    dest => dest.HowOldIsBook,
                    opt => opt.MapFrom(src => DateTime.Now.Year - src.PublishedDate.Year)
                 )
                .ForMember(
                    dest => dest.AuthorName,
                    opt => opt.MapFrom(src => src.Author.FullName)
                 )
                .ForMember(
                    dest => dest.PublisherName,
                    opt => opt.MapFrom(src => src.Publisher.Name)
                );


            CreateMap<Book, BookDetailsDto>()
                .ForMember(
                    dest => dest.HowOldIsBook,
                    opt => opt.MapFrom(src => DateTime.Now.Year - src.PublishedDate.Year)
                 )
                .ForMember(
                    dest => dest.AuthorName,
                    opt => opt.MapFrom(src => src.Author.FullName)
                 )
                .ForMember(
                    dest => dest.PublisherName,
                    opt => opt.MapFrom(src => src.Publisher.Name)
                );
        }
    }

}
