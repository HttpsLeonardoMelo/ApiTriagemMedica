using Data.Models;

namespace Business.TransferObjects
{
	public class MedicoDto
	{
		public Guid Id { get; set; }  // uniqueidentifier
		public string Nome { get; set; }  // varchar(255)
		public bool Disponivel { get; set; }  // bit
		public bool Ativo { get; set; }  // bit
		public string? CrmUf { get; set; }  // varchar(13)

		// Relacionamentos
		public Especialidade? Especialidade { get; set; }
	}
}
