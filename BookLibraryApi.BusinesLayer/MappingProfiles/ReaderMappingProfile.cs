using AutoMapper;
using BookLibraryApi.BusinesLayer.ViewModels;
using BookLibraryApi.DataAccess.Entities;

namespace BookLibraryApi.BusinesLayer.MappingProfiles
{
    public class ReaderMappingProfile : Profile
    {
        public ReaderMappingProfile()
        {
            CreateMap<Reader, ReaderViewModel>();
            CreateMap<ReaderViewModel, Reader>();
        }
    }
}
