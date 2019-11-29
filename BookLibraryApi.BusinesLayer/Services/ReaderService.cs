using AutoMapper;
using BookLibraryApi.BusinesLayer.ViewModels;
using BookLibraryApi.DataAccess.Entities;
using BookLibraryApi.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibraryApi.BusinesLayer.Services
{
    public class ReaderService:BaseService
    {
        private readonly IRepository<Reader> _readerRepository;
        public ReaderService(IMapper mapper,IRepository<Reader> repository):base(mapper)
        {
            _readerRepository = repository;
        }

        //implement extra CRUD logic here
        public void Create(ReaderViewModel book)
        {
            Reader mapped = _mapper.Map<Reader>(book);
            _readerRepository.Create(mapped);
        }

        public void Delete(ReaderViewModel book)
        {
            Reader mapped = _mapper.Map<Reader>(book);
            _readerRepository.Delete(mapped);
        }

        public void Update(ReaderViewModel book)
        {
            Reader mapped = _mapper.Map<Reader>(book);
            _readerRepository.Update(mapped);
        }

        public IEnumerable<ReaderViewModel> Get()
        {
            IEnumerable<ReaderViewModel> books = _mapper.Map<IEnumerable<ReaderViewModel>>(_readerRepository.Get());
            return books;
        }

        public ReaderViewModel Get(int id)
        {
            ReaderViewModel books = _mapper.Map<ReaderViewModel>(_readerRepository.Get(id));
            return books;
        }
    }
}
