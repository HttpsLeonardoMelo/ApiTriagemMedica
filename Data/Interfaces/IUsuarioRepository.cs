using Data.Models;
using Data.Models.Filtros;
using Data.Models.Listas;

namespace Data.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> Add(Usuario usuario);
        Task<Usuario> Update(Usuario usuario);
        Task<IEnumerable<Usuario>> GetAll(bool listaCompleta = false);
        Task<Usuario> GetById(Guid id);
        Task<Usuario> GetByEmail(string email, bool somenteAtivos = false);
        Task<IEnumerable<Usuario>> GetByNome(string nome, bool somenteAtivos = false);
        Task<ListaPaginada<ListaUsuario>> ListarUsuariosAsync(FiltroUsuario filtro, bool paginarRegistros = true);
        Task<string> Delete(Guid id);
        Task<bool> EmailExiste(Guid? id, string email);
        Task<Usuario> UpdateSenha(Usuario usuario);
    }
}
