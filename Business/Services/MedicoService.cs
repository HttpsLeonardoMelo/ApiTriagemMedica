using AutoMapper;
using Business.Interfaces;
using Business.TransferObjects;
using Data.Interfaces;
using Data.Models;

namespace Business.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly IMapper _mapper;
        private readonly IMedicoRepository _repoMedico;

        public MedicoService(IMapper mapper, IMedicoRepository repoMedico)
        {
            _mapper = mapper;
            _repoMedico = repoMedico;
        }

        public async Task<MedicoDto> Add(MedicoDto medico)
        {
            return _mapper.Map<MedicoDto>(await _repoMedico.Add(_mapper.Map<Medico>(medico)));
        }

        public async Task<MedicoDto> Update(MedicoDto medico)
        {
            return _mapper.Map<MedicoDto>(await _repoMedico.Update(_mapper.Map<Medico>(medico)));
        }

        public async Task<IEnumerable<MedicoDto>> GetAll()
        {
            return _mapper.Map<List<MedicoDto>>(await _repoMedico.GetAll());
        }

        public async Task<MedicoDto> GetById(Guid id)
        {
            return _mapper.Map<MedicoDto>(await _repoMedico.GetById(id));
        }

        public async Task<string> Delete(Guid id)
        {
            return _mapper.Map<string>(await _repoMedico.Delete(id));
        }
    }
}
