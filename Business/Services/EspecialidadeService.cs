using AutoMapper;
using Business.Interfaces;
using Business.TransferObjects;
using Data.Interfaces;
using Data.Models;

namespace Business.Services
{
    public class EspecialidadeService : IEspecialidadeService
    {
        private readonly IMapper _mapper;
        private readonly IEspecialidadeRepository _repoEspecialidade;

        public EspecialidadeService(IMapper mapper, IEspecialidadeRepository repoEspecialidade)
        {
            _mapper = mapper;
            _repoEspecialidade = repoEspecialidade;
        }

        public async Task<EspecialidadeDto> Add(EspecialidadeDto especialidade)
        {
            return _mapper.Map<EspecialidadeDto>(await _repoEspecialidade.Add(_mapper.Map<Especialidade>(especialidade)));
        }

        public async Task<EspecialidadeDto> Update(EspecialidadeDto especialidade)
        {
            return _mapper.Map<EspecialidadeDto>(await _repoEspecialidade.Update(_mapper.Map<Especialidade>(especialidade)));
        }

        public async Task<IEnumerable<EspecialidadeDto>> GetAll()
        {
            return _mapper.Map<List<EspecialidadeDto>>(await _repoEspecialidade.GetAll());
        }

        public async Task<EspecialidadeDto> GetById(Guid id)
        {
            return _mapper.Map<EspecialidadeDto>(await _repoEspecialidade.GetById(id));
        }

        public async Task<string> Delete(Guid id)
        {
            return _mapper.Map<string>(await _repoEspecialidade.Delete(id));
        }
    }
}
