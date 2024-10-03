using Business.TransferObjects;


namespace Business.Interfaces
{
    public interface ITriagemService
    {
        Task<TriagemDto> Add(TriagemDto triagem);
        Task<TriagemDto> Update(TriagemDto triagem);
        Task<IEnumerable<TriagemDto>> GetAll();
        Task<TriagemDto> GetById(Guid id);
        Task<string> Delete(Guid id);
    }
}
