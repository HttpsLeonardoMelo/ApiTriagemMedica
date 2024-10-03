using Business.TransferObjects;
using Data.Models.ViewModels;

namespace Business.Interfaces
{
    public interface IAtendimentoService
    {
        Task<AtendimentoDto> Add(AtendimentoDto atendimento);
        Task<string> AtualizarStatus(Guid id, string status);
		Task<IEnumerable<AtendimentoDto>> GetAll();
        Task<IEnumerable<ChamadaPaciente>> Fila();
		Task<ChamadaPaciente> ChamarProximo(string status);
		Task<AtendimentoDto> GetById(Guid id);
        Task<string> Delete(Guid id);
    }
}
