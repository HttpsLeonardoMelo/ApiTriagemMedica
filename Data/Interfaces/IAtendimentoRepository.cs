using Data.Models;
using Data.Models.ViewModels;


namespace Data.Interfaces
{
	public interface IAtendimentoRepository
	{
		Task<Atendimento> Add(Atendimento atendimento);
		Task<string> AtualizarStatus(Guid id, string status);
		Task<IEnumerable<Atendimento>> GetAll();
		Task<IEnumerable<ChamadaPaciente>> Fila();
		Task<Atendimento> GetById(Guid id);
		Task<string> Delete(Guid id);
		Task<ChamadaPaciente> ChamarProximo(string status);
	}
}
