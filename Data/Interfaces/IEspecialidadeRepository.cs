using Data.Models.Filtros;
using Data.Models.Listas;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
	public interface IEspecialidadeRepository
	{
		Task<Especialidade> Add(Especialidade especialidade);
		Task<Especialidade> Update(Especialidade especialidade);
		Task<IEnumerable<Especialidade>> GetAll();
		Task<Especialidade> GetById(Guid id);
		Task<string> Delete(Guid id);
	}
}
