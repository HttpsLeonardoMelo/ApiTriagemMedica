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
	public interface IPacienteRepository
	{
		Task<Paciente> Add(Paciente paciente);
		Task<Paciente> Update(Paciente paciente);
		Task<IEnumerable<Paciente>> GetAll();
		Task<Paciente> GetById(Guid id);
		Task<string> Delete(Guid id);
	}
}
