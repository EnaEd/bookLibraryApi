using AutoMapper;
using BookLibraryApi.BusinesLayer.Intefaces;
using BookLibraryApi.BusinesLayer.ViewModels;
using BookLibraryApi.DataAccess.Entities;
using BookLibraryApi.DataAccess.Interfaces;
using System.Collections.Generic;

namespace BookLibraryApi.BusinesLayer.Services
{
    public class ReaderService : BaseService, IService<ReaderViewModel>
    {
        private readonly IRepository<Reader> _readerRepository;
        public ReaderService(IMapper mapper, IRepository<Reader> repository) : base(mapper)
        {
            _readerRepository = repository;
        }

        //implement extra CRUD logic here
        public void Create(ReaderViewModel reader)
        {
            Reader mapped = _mapper.Map<Reader>(reader);
            _readerRepository.Create(mapped);
        }

        public void Delete(ReaderViewModel reader)
        {
            Reader mapped = _mapper.Map<Reader>(reader);
            _readerRepository.Delete(mapped);
        }

        public void Update(ReaderViewModel reader)
        {
            Reader mapped = _mapper.Map<Reader>(reader);
            _readerRepository.Update(mapped);
        }

        public IEnumerable<ReaderViewModel> Get()
        {
            IEnumerable<ReaderViewModel> readers = _mapper.Map<IEnumerable<ReaderViewModel>>(_readerRepository.Get());
            return readers;
        }

        public ReaderViewModel Get(int id)
        {
            ReaderViewModel readers = _mapper.Map<ReaderViewModel>(_readerRepository.Get(id));
            return readers;
        }

        public IEnumerable<ReaderViewModel> Get(string id)
        {
            return null;
        }
    }
}
