using Business.TransferObjects;

namespace Business.Interfaces
{
    public interface IEspecialidadeService
    {
        Task<EspecialidadeDto> Add(EspecialidadeDto especialidade);
        Task<EspecialidadeDto> Update(EspecialidadeDto especialidade);
        Task<IEnumerable<EspecialidadeDto>> GetAll();
        Task<EspecialidadeDto> GetById(Guid id);
        Task<string> Delete(Guid id);
    }
}
