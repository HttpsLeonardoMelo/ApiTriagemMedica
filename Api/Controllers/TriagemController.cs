using Business.Interfaces;
using Business.TransferObjects;
using Business.TransferObjects.Mensagens;
using Data.Constantes;
using Data.Interfaces.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TriagemController : BaseController
    {
        public IVerificacoes _verificacoes;

        public TriagemController(ILogger<BaseController> logger, IVerificacoes verificacoes) : base(logger)
        {
            _verificacoes = verificacoes;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TriagemDto triagem, [FromServices] ITriagemService _service, [FromServices] IAtendimentoService _atendimentoService)
        {
            try
            {
				TriagemDto triagemAdd = await _service.Add(triagem);

                await _atendimentoService.AtualizarStatus(triagem.AtendimentoId, "Aguardando Consulta");

				return Ok(new MensagemSucessoDto(Resources.INCLUIDO_SUCESSO, Resources.STATUS_OK));

            }
            catch (Exception ex)
            {
                return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message, Resources.STATUS_BAD_REQUEST));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TriagemDto triagem, [FromServices] ITriagemService _service)
        {
            try
            {
                TriagemDto triagemAdd = await _service.Update(triagem);

                return Ok(new MensagemSucessoDto(Resources.ATUALIZADO_SUCESSO, Resources.STATUS_OK));
   

            }
            catch (Exception ex)
            {
                return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message, Resources.STATUS_BAD_REQUEST));
            }
        }

		[HttpGet]
		public async Task<IActionResult> GetAll([FromServices] ITriagemService _service)
		{
			try
			{
				IEnumerable<TriagemDto> triagems = await _service.GetAll();

				if (triagems != null)
				{
					return Ok(new MensagemSucessoDto(Resources.BUSCA_SUCESSO, Resources.STATUS_OK, triagems));
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message, Resources.STATUS_BAD_REQUEST));
			}
		}

		[HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromServices] ITriagemService _service, [FromQuery] Guid id)
        {
            try
            {
                TriagemDto triagem = await _service.GetById(id);

                if (triagem != null)
                {
					return Ok(new MensagemSucessoDto(Resources.BUSCA_SUCESSO, Resources.STATUS_OK, triagem));
				}
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message, Resources.STATUS_BAD_REQUEST));
            }
        }

		[HttpDelete]
		public async Task<IActionResult> Delete([FromServices] ITriagemService _service, [FromQuery] Guid id)
		{
			try
			{
                await _service.Delete(id);

		        return Ok(new MensagemSucessoDto(Resources.DELETE_SUCESSO, Resources.STATUS_OK));

			}
			catch (Exception ex)
			{
				return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message, Resources.STATUS_BAD_REQUEST));
			}
		}
	}
}
