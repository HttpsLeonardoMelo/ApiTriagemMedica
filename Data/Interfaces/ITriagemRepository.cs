using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
	public interface ITriagemRepository
	{
		Task<Triagem> Add(Triagem triagem);
		Task<Triagem> Update(Triagem triagem);
		Task<IEnumerable<Triagem>> GetAll();
		Task<Triagem> GetById(Guid id);
		Task<string> Delete(Guid id);
	}
}
