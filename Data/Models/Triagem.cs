using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
	public class Triagem
	{
		public Guid TriagemId { get; set; }  // uniqueidentifier
		public Guid AtendimentoId { get; set; }  // uniqueidentifier
		public string Sintomas { get; set; }  // varchar(1000)
		public decimal PressaoSistolica { get; set; }  // decimal(5, 1)
		public decimal PressaoDiastolica { get; set; }  // decimal(5, 1)
		public decimal Peso { get; set; }  // decimal(5, 1)
		public decimal Altura { get; set; }  // decimal(3, 2)
		public Guid EspecialidadeId { get; set; }  // uniqueidentifier

		// Relacionamentos
		public Atendimento? Atendimento { get; set; }
		public Especialidade? Especialidade { get; set; }
	}

}
