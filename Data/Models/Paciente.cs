using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
		public class Paciente
		{
			public Guid PacienteId { get; set; }  // uniqueidentifier
			public string Nome { get; set; }  // varchar(255)
			public char Sexo { get; set; }  // char(1)
			public string Email { get; set; }  // varchar(255)
		}

}
