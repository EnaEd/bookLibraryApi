using AutoMapper;
using BookLibraryApi.BusinesLayer.ViewModels;
using BookLibraryApi.DataAccess.Entities;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BookLibraryApi.BusinesLayer.MappingProfiles
{
    public class ReaderMappingProfile : Profile
    {
        public ReaderMappingProfile()
        {
            CreateMap<Reader, ReaderViewModel>()
                .ForMember(x => x.Books, o => o.MapFrom(s => s.Books.Select(x=> new Book { Id=x.Id,ReaderId=x.ReaderId,Author=x.Author,Category=x.Category,Title=x.Title} )));
                

            CreateMap<ReaderViewModel, Reader>()
                .ForMember(x => x.Books, o => o.MapFrom(s => s.Books));
                
        }
    }
}
