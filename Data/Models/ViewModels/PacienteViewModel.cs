
namespace Data.Models.ViewModels
{
	public class ChamadaPaciente
	{
		public Guid AtendimentoId { get; set; }
		public int Token { get; set; }
		public string Nome { get; set; }
		public Guid PacienteId { get; set; }
	}
}
