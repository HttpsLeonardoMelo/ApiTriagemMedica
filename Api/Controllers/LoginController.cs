using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Api.Provider;
using Data.Constantes;
using Business.TransferObjects.Mensagens;
using Business.TransferObjects.ViewModels;
using Business.Interfaces;
using Business.TransferObjects;
using Data.Interfaces.Util;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : BaseController
    {
        private readonly TokenSettings _tokenSettings;

        public LoginController(ILogger<BaseController> logger, IOptions<TokenSettings> tokenSettings) : base(logger)
        {
            _tokenSettings = tokenSettings.Value;
        }


        [Route("AutenticarUsuario")]
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]
        //Verifica se usuário é válido para receber um token. Caso seja, retorna um token. Qualquer usuário pode requisitá-lo.
        public async Task<IActionResult> AutenticarUsuario([FromBody] LoginDto data, [FromServices] IUsuarioService _repo, [FromServices] IHashSenha _hashSenha)
        {
            try
            {
                if (string.IsNullOrEmpty(data.Email))
                {
                    return Conflict(new MensagemSucessoDto(Resources.USUARIO_EMAIL_NULL, Resources.STATUS_CONFLICT));
                }

                //Buscando usuário pelo email digitado.
                UsuarioDto usuario = await _repo.GetByEmail(data.Email, false);

                //Verificando se usuário foi encontrado.
                if ((usuario != null) && (usuario.Email.ToLower() == data.Email.ToLower() && usuario.Senha == _hashSenha.Hash(data.Senha)))
                {
                    //Construindo token usando dados de usuário e guardados em _tokenSettings.
                    var token = new TokenBuilder()
                        .AddSecurityKey(SecurityKeyGen.Create(_tokenSettings.Secret))  //Passando chave secreta para ser encriptada guardada no token.
                        .AddSubject(usuario.Nome)
                        .AddIssuer(_tokenSettings.Issuer) //Emissor = UserName da aplicação.
                        .AddAudience(_tokenSettings.ValidIn) //ValidoEm = Url(s) que pode(m) chamar a API.
                        .AddClaim("UserId", usuario.Id.ToString())
                        .AddExpiry(_tokenSettings.ExpirationMinutes) //ExpiracaoMinutos = Tempo de validade do token (60 minutos : 1 hrs).
                        .Builder(); //Metodo que compila o token e o torna utilizavel.


					return Ok(new MensagemSucessoDto(Resources.USUARIO_AUTORIZADO, Resources.STATUS_OK, new { token = token.value, expiracaoToken = token.ValidTo }));
                   
                }
                else
                {
                    return Conflict(new MensagemErroDto(Resources.USUARIO_NAO_AUTORIZADO, Resources.STATUS_CONFLICT));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO, Resources.STATUS_BAD_REQUEST, ex));
            }

        }
	}
}
