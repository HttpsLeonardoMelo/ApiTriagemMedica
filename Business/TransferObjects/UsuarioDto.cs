
using System.ComponentModel.DataAnnotations;

namespace Business.TransferObjects
{
    public class UsuarioDto
    {
        public Guid Id { get; set; }
        [MaxLength(255)]
        public string Nome { get; set; }
		[MaxLength(255)]
		public string Email { get; set; }
		[MaxLength(255)]
		public string Senha { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; } 
        public DateTime? DataUltimoLogin { get; set; }

        //Campos formatados
        public string AtivoFormatado { get { return Ativo ? "Ativo" : "Inativo"; } }
        public string DataCriacaoFormatada { get { return DataCriacao.ToString("dd/MM/yyyy HH:mm:ss"); } }
        public string DataUltimoLoginFormatada { get { return Convert.ToDateTime(DataUltimoLogin).ToString("dd/MM/yyyy HH:mm:ss"); } }
    }
}
