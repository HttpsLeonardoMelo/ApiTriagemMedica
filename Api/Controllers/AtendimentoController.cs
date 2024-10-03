using Business.Interfaces;
using Business.TransferObjects;
using Business.TransferObjects.Mensagens;
using Data.Constantes;
using Data.Interfaces.Util;
using Data.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AtendimentoController : BaseController
    {
        public IVerificacoes _verificacoes;

        public AtendimentoController(ILogger<BaseController> logger, IVerificacoes verificacoes) : base(logger)
        {
            _verificacoes = verificacoes;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AtendimentoDto atendimento, [FromServices] IAtendimentoService _service)
        {
            try
            {
				AtendimentoDto atendimentoAdd = await _service.Add(atendimento);

                return Ok(new MensagemSucessoDto(Resources.INCLUIDO_SUCESSO, Resources.STATUS_OK));

            }
            catch (Exception ex)
            {
                return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message, Resources.STATUS_BAD_REQUEST));
            }
        }

		[HttpGet]
		public async Task<IActionResult> GetAll([FromServices] IAtendimentoService _service)
		{
			try
			{
				IEnumerable<AtendimentoDto> atendimentos = await _service.GetAll();

				if (atendimentos != null)
				{
					return Ok(new MensagemSucessoDto(Resources.BUSCA_SUCESSO, Resources.STATUS_OK, atendimentos));
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

		[HttpGet("Fila")]
		public async Task<IActionResult> Fila([FromServices] IAtendimentoService _service)
		{
			try
			{
				IEnumerable<ChamadaPaciente> atendimentos = await _service.Fila();

				if (atendimentos != null)
				{
					return Ok(new MensagemSucessoDto(Resources.BUSCA_SUCESSO, Resources.STATUS_OK, atendimentos));
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

		[HttpGet("ChamarProximo")]
        public async Task<IActionResult> ChamarProximo([FromServices] IAtendimentoService _atendimentoService)
        {
            try
            {
                ChamadaPaciente chamada = await _atendimentoService.ChamarProximo("Aguardando Atendimento");

				await _atendimentoService.AtualizarStatus(chamada.AtendimentoId, "Em consulta");

                return Ok(new MensagemSucessoDto(Resources.BUSCA_SUCESSO, Resources.STATUS_OK, chamada));
			}
			catch (Exception ex)
			{
				return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message, Resources.STATUS_BAD_REQUEST));
			}
		}

		[HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromServices] IAtendimentoService _service, [FromQuery] Guid id)
        {
            try
            {
                AtendimentoDto atendimento = await _service.GetById(id);

                if (atendimento != null)
                {
					return Ok(new MensagemSucessoDto(Resources.BUSCA_SUCESSO, Resources.STATUS_OK, atendimento));
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
		public async Task<IActionResult> Delete([FromServices] IAtendimentoService _service, [FromQuery] Guid id)
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
