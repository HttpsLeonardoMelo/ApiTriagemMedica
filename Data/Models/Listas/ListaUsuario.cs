
namespace Data.Models.Listas
{
    public class ListaUsuario
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Foto { get; set; }
        public bool Ativo { get; set; }
        public int Nivel { get; set; }
        public string Departamento { get; set; }
        public DateTime? DataUltimoLogin { get; set; }
        public DateTime? DataCriacao { get; set; }

        //Campos formatados
        public string AtivoFormatado { get { return Ativo ? "Ativo" : "Inativo"; } }
        public string DataUltimoLoginFormatada { get { return Convert.ToDateTime(DataUltimoLogin).ToString("dd/MM/yyyy HH:mm:ss"); } }
        public string DataCriacaoFormatada { get { return Convert.ToDateTime(DataCriacao).ToString("dd/MM/yyyy HH:mm:ss"); } }
    }
}
