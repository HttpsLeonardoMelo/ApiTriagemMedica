
using Business.TransferObjects;

namespace Business.Interfaces
{
    public interface IPacienteService
    {
        Task<PacienteDto> Add(PacienteDto paciente);
        Task<PacienteDto> Update(PacienteDto paciente);
        Task<IEnumerable<PacienteDto>> GetAll();
        Task<PacienteDto> GetById(Guid id);
        Task<string> Delete(Guid id);
    }
}
