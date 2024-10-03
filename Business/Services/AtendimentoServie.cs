using AutoMapper;
using Business.Interfaces;
using Business.TransferObjects;
using Data.Interfaces;
using Data.Models;
using Data.Models.ViewModels;

namespace Business.Services
{
    public class AtendimentoService : IAtendimentoService
    {
        private readonly IMapper _mapper;
        private readonly IAtendimentoRepository _repoAtendimento;

        public AtendimentoService(IMapper mapper, IAtendimentoRepository repoAtendimento)
        {
            _mapper = mapper;
            _repoAtendimento = repoAtendimento;
        }

        public async Task<AtendimentoDto> Add(AtendimentoDto atendimento)
        {
            return _mapper.Map<AtendimentoDto>(await _repoAtendimento.Add(_mapper.Map<Atendimento>(atendimento)));
        }

		public async Task<string> AtualizarStatus(Guid id, string status)
		{
			return await _repoAtendimento.AtualizarStatus(id, status);
		}

        public async Task<ChamadaPaciente> ChamarProximo(string status)
        {
            return await _repoAtendimento.ChamarProximo(status);
        }

		public async Task<IEnumerable<AtendimentoDto>> GetAll()
        {
            return _mapper.Map<List<AtendimentoDto>>(await _repoAtendimento.GetAll());
        }

		public async Task<IEnumerable<ChamadaPaciente>> Fila()
		{
            return await _repoAtendimento.Fila();
        }

        public async Task<AtendimentoDto> GetById(Guid id)
        {
            return _mapper.Map<AtendimentoDto>(await _repoAtendimento.GetById(id));
        }

        public async Task<string> Delete(Guid id)
        {
            return _mapper.Map<string>(await _repoAtendimento.Delete(id));
        }
	}
}
