using AutoMapper;
using Business.Interfaces;
using Business.TransferObjects;
using Data.Interfaces;
using Data.Models;

namespace Business.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IMapper _mapper;
        private readonly IPacienteRepository _repoPaciente;

        public PacienteService(IMapper mapper, IPacienteRepository repoPaciente)
        {
            _mapper = mapper;
            _repoPaciente = repoPaciente;
        }

        public async Task<PacienteDto> Add(PacienteDto paciente)
        {
            return _mapper.Map<PacienteDto>(await _repoPaciente.Add(_mapper.Map<Paciente>(paciente)));
        }

        public async Task<PacienteDto> Update(PacienteDto paciente)
        {
            return _mapper.Map<PacienteDto>(await _repoPaciente.Update(_mapper.Map<Paciente>(paciente)));
        }

        public async Task<IEnumerable<PacienteDto>> GetAll()
        {
            return _mapper.Map<List<PacienteDto>>(await _repoPaciente.GetAll());
        }

        public async Task<PacienteDto> GetById(Guid id)
        {
            return _mapper.Map<PacienteDto>(await _repoPaciente.GetById(id));
        }

        public async Task<string> Delete(Guid id)
        {
            return _mapper.Map<string>(await _repoPaciente.Delete(id));
        }
    }
}
