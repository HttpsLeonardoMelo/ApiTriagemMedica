using AutoMapper;
using Business.Interfaces;
using Business.TransferObjects;
using Business.TransferObjects.Mensagens;
using Data.Constantes;
using Data.Interfaces.Util;
using Data.Models.Filtros;
using Data.Models.Listas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsuarioController : BaseController
    {
        public IVerificacoes _verificacoes;

        public UsuarioController(ILogger<BaseController> logger, IVerificacoes verificacoes) : base(logger)
        {
            _verificacoes = verificacoes;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioDto usuario, [FromServices] IUsuarioService _service)
        {
            try
            {
                if (await _service.EmailExiste(null, usuario.Email))
                    return Conflict(new MensagemErroDto(Resources.EMAIL_EXISTE, Resources.STATUS_CONFLICT, new { campoErro = "Email" }));

                if (!_verificacoes.EmailValido(usuario.Email))
                    return Conflict(new MensagemErroDto(Resources.CAMPO_INVALIDO, Resources.STATUS_CONFLICT, new { campoErro = "Email" }));

                if (!_verificacoes.SenhaValida(usuario.Senha))
                    return Conflict(new MensagemErroDto(Resources.CAMPO_INVALIDO, Resources.STATUS_CONFLICT, new { campoErro = "Senha" }));

				UsuarioDto usuarioAdd = await _service.Add(usuario);

                return Ok(new MensagemSucessoDto(Resources.INCLUIDO_SUCESSO, Resources.STATUS_OK));

            }
            catch (Exception ex)
            {
                return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message, Resources.STATUS_BAD_REQUEST));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UsuarioDto usuario, [FromServices] IUsuarioService _service)
        {
            try
            {
                if (await _service.EmailExiste(usuario.Id, usuario.Email))
                {
                    return Conflict(new MensagemErroDto(Resources.EMAIL_EXISTE, Resources.STATUS_CONFLICT));
                }

                UsuarioDto usuarioAdd = await _service.Update(usuario);


                return Ok(new MensagemSucessoDto(Resources.ATUALIZADO_SUCESSO, Resources.STATUS_OK));
   

            }
            catch (Exception ex)
            {
                return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message, Resources.STATUS_BAD_REQUEST));
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromServices] IUsuarioService _service, [FromQuery] Guid id)
        {
            try
            {
                UsuarioDto usuario = await _service.GetById(id);

				return Ok(new MensagemSucessoDto(Resources.BUSCA_SUCESSO, Resources.STATUS_OK, usuario));

            }
            catch (Exception ex)
            {
                return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message, Resources.STATUS_BAD_REQUEST));
            }
        }

        [HttpGet("GetByEmail")]
        public async Task<IActionResult> GetByEmail([FromServices] IUsuarioService _service, [FromQuery] string email, bool somenteAtivos = false)
        {
            try
            {
                UsuarioDto usuario = await _service.GetByEmail(email, somenteAtivos);


				return Ok(new MensagemSucessoDto(Resources.BUSCA_SUCESSO, Resources.STATUS_OK, usuario));

            }
            catch (Exception ex)
            {
                return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message, Resources.STATUS_BAD_REQUEST));
            }
        }

        [HttpGet("GetByNome")]
        public async Task<IActionResult> GetByNome([FromServices] IUsuarioService _service, [FromQuery] string nome, bool somenteAtivos = false)
        {
            try
            {
                IEnumerable<UsuarioDto> usuarios = await _service.GetByNome(nome, somenteAtivos);


				return Ok(new MensagemSucessoDto(Resources.BUSCA_SUCESSO, Resources.STATUS_OK, usuarios));
            }
            catch (Exception ex)
            {
                return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message));
            }
        }

        [HttpPost("Listar")]
        public async Task<IActionResult> ListarUsuario(
           [FromBody] FiltroUsuario filtro,
           [FromServices] IUsuarioService _service, [FromServices] IMapper mapper)
        {
            try
            {
                    ListaPaginada<ListaUsuario> lista = await _service.ListarUsuariosAsync(filtro);

                    return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message, Resources.STATUS_BAD_REQUEST));
            }
        }

        [HttpPut("Inativar")]
        public async Task<IActionResult> Inativar([FromQuery] Guid id, [FromServices] IUsuarioService _service)
        {
            try
            {
                    UsuarioDto usuario = await _service.GetById(id);
                    usuario.Ativo = false;
                    await _service.Update(usuario);
                    return Ok(new MensagemSucessoDto(Resources.INATIVADO_SUCESSO, Resources.STATUS_OK));
            }
            catch (Exception ex)
            {
                return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message, Resources.STATUS_BAD_REQUEST));
            }
        }
    }
}
