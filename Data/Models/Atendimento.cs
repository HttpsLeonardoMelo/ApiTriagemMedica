
namespace Data.Models
{
	public class Atendimento
	{
		public Guid AtendimentoId { get; set; }  // uniqueidentifier
		public Guid PacienteId { get; set; }  // uniqueidentifier
		public int? Token { get; set; }  // int
		public DateTime? DataHoraChegada { get; set; }  // datetime
		public string? Status { get; set; }  // Atendido, Aguardando Atendimento, Deisitiu
	}
}
