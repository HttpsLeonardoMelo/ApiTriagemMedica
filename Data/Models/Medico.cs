using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
	public class Medico
	{
		public Guid MedicoId { get; set; }  // uniqueidentifier
		public Guid EspecialidadeId { get; set; }  // uniqueidentifier
		public string Nome { get; set; }  // varchar(255)
		public bool Disponivel { get; set; }  // bit
		public bool Ativo { get; set; }  // bit
		public string? CrmUf { get; set; }  // varchar(13)

		// Relacionamentos
		public Especialidade? Especialidade { get; set; }
	}

}
