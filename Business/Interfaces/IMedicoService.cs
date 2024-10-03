
using Business.TransferObjects;

namespace Business.Interfaces
{
    public interface IMedicoService
    {
        Task<MedicoDto> Add(MedicoDto medico);
        Task<MedicoDto> Update(MedicoDto medico);
        Task<IEnumerable<MedicoDto>> GetAll();
        Task<MedicoDto> GetById(Guid id);
        Task<string> Delete(Guid id);
    }
}
