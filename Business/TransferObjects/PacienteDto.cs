
namespace Business.TransferObjects
{
	public class PacienteDto
	{
		public Guid Id { get; set; }  // uniqueidentifier
		public string Nome { get; set; }  // varchar(255)
		public char Sexo { get; set; }  // char(1)
		public string Email { get; set; }  // varchar(255)
	}
}
