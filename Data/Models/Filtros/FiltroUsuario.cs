using Data.Models.Enums;

namespace Data.Models.Filtros
{
    public class FiltroUsuario
    {
        public string Busca {  get; set; }
        public UsuarioEnum OrdenarPor { get; set; }
        public AscDescEnum Ordem { get; set; }
        public int Pagina { get; set; }
        public int ItensPagina { get; set; }
    }
}
