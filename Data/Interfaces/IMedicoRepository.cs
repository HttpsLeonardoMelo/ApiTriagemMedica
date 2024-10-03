using Data.Models;

namespace Data.Interfaces
{
	public interface IMedicoRepository
	{
		Task<Medico> Add(Medico medico);
		Task<Medico> Update(Medico medico);
		Task<IEnumerable<Medico>> GetAll();
		Task<Medico> GetById(Guid id);
		Task<string> Delete(Guid id);
	}
}
