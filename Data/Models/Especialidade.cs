using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
	public class Especialidade
	{
		public Guid EspecialidadeId { get; set; }  // uniqueidentifier
		public string Nome { get; set; }  // varchar(255)
		public string Descricao { get; set; }  // varchar(255) (pode ser nulo)
		public bool Ativo { get; set; }  // bit

	}

}
