using Business.TransferObjects;
using Data.Models.Filtros;
using Data.Models.Listas;

namespace Business.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDto> Add(UsuarioDto usuario);
        Task<UsuarioDto> Update(UsuarioDto usuario);
        Task<IEnumerable<UsuarioDto>> GetAll(bool listaCompleta = false);
        Task<UsuarioDto> GetById(Guid id);
        Task<UsuarioDto> GetByEmail(string email, bool somenteAtivos = false);
        Task<IEnumerable<UsuarioDto>> GetByNome(string nome, bool somenteAtivos = false);
        Task<ListaPaginada<ListaUsuario>> ListarUsuariosAsync(FiltroUsuario filtro);
        Task<string> Delete(Guid id);
        Task<bool> EmailExiste(Guid? id, string email);
        Task<UsuarioDto> UpdateSenha(UsuarioDto usuario);
    }
}
