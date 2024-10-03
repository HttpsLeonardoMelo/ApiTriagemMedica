using AutoMapper;
using Business.Interfaces;
using Business.TransferObjects;
using Data.Interfaces;
using Data.Models;

namespace Business.Services
{
    public class TriagemService : ITriagemService
    {
        private readonly IMapper _mapper;
        private readonly ITriagemRepository _repoTriagem;

        public TriagemService(IMapper mapper, ITriagemRepository repoTriagem)
        {
            _mapper = mapper;
            _repoTriagem = repoTriagem;
        }

        public async Task<TriagemDto> Add(TriagemDto triagem)
        {
            return _mapper.Map<TriagemDto>(await _repoTriagem.Add(_mapper.Map<Triagem>(triagem)));
        }

        public async Task<TriagemDto> Update(TriagemDto triagem)
        {
            return _mapper.Map<TriagemDto>(await _repoTriagem.Update(_mapper.Map<Triagem>(triagem)));
        }

        public async Task<IEnumerable<TriagemDto>> GetAll()
        {
            return _mapper.Map<List<TriagemDto>>(await _repoTriagem.GetAll());
        }

        public async Task<TriagemDto> GetById(Guid id)
        {
            return _mapper.Map<TriagemDto>(await _repoTriagem.GetById(id));
        }

        public async Task<string> Delete(Guid id)
        {
            return _mapper.Map<string>(await _repoTriagem.Delete(id));
        }
    }
}
