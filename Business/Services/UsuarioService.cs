using AutoMapper;
using Business.Interfaces;
using Business.TransferObjects;
using Data.Interfaces;
using Data.Interfaces.Util;
using Data.Models;
using Data.Models.Filtros;
using Data.Models.Listas;
using Data.Util;

namespace Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _repoUsuario;

        public UsuarioService(IMapper mapper, IUsuarioRepository repoUsuario)
        {
            _mapper = mapper;
            _repoUsuario = repoUsuario;
        }

        public async Task<UsuarioDto> Add(UsuarioDto Usuario)
        {
            return _mapper.Map<UsuarioDto>(await _repoUsuario.Add(_mapper.Map<Usuario>(Usuario)));
        }

        public async Task<UsuarioDto> Update(UsuarioDto Usuario)
        {
            return _mapper.Map<UsuarioDto>(await _repoUsuario.Update(_mapper.Map<Usuario>(Usuario)));
        }

        public async Task<IEnumerable<UsuarioDto>> GetAll(bool listaCompleta = false)
        {
            return _mapper.Map<List<UsuarioDto>>(await _repoUsuario.GetAll(listaCompleta));
        }

        public async Task<UsuarioDto> GetById(Guid id)
        {
            return _mapper.Map<UsuarioDto>(await _repoUsuario.GetById(id));
        }

        public async Task<UsuarioDto> GetByEmail(string email, bool somenteAtivos = false)
        {
            return _mapper.Map<UsuarioDto>(await _repoUsuario.GetByEmail(email, somenteAtivos));
        }
        public async Task<IEnumerable<UsuarioDto>> GetByNome(string nome, bool somenteAtivos = false)
        {
            return _mapper.Map<List<UsuarioDto>>(await _repoUsuario.GetByNome(nome, somenteAtivos));
        }
        public async Task<ListaPaginada<ListaUsuario>> ListarUsuariosAsync(FiltroUsuario filtro)
        {
            return await _repoUsuario.ListarUsuariosAsync(filtro);
        }
        public async Task<string> Delete(Guid id)
        {
            return _mapper.Map<string>(await _repoUsuario.Delete(id));
        }

        public async Task<bool> EmailExiste(Guid? id, string email)
        {
            return await _repoUsuario.EmailExiste(id, email);
        }

        public async Task<UsuarioDto> UpdateSenha(UsuarioDto Usuario)
        {
            return _mapper.Map<UsuarioDto>(await _repoUsuario.UpdateSenha(_mapper.Map<Usuario>(Usuario)));
        }
    }
}
