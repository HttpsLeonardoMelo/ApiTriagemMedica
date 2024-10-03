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
    public class MedicoController : BaseController
    {
        public IVerificacoes _verificacoes;

        public MedicoController(ILogger<BaseController> logger, IVerificacoes verificacoes) : base(logger)
        {
            _verificacoes = verificacoes;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MedicoDto medico, [FromServices] IMedicoService _service)
        {
            try
            {
				MedicoDto medicoAdd = await _service.Add(medico);

                return Ok(new MensagemSucessoDto(Resources.INCLUIDO_SUCESSO, Resources.STATUS_OK));

            }
            catch (Exception ex)
            {
                return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message, Resources.STATUS_BAD_REQUEST));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MedicoDto medico, [FromServices] IMedicoService _service)
        {
            try
            {
                MedicoDto medicoAdd = await _service.Update(medico);

                return Ok(new MensagemSucessoDto(Resources.ATUALIZADO_SUCESSO, Resources.STATUS_OK));
   

            }
            catch (Exception ex)
            {
                return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message, Resources.STATUS_BAD_REQUEST));
            }
        }

		[HttpGet]
		public async Task<IActionResult> GetAll([FromServices] IMedicoService _service)
		{
			try
			{
				IEnumerable<MedicoDto> medicos = await _service.GetAll();

				if (medicos != null)
				{
					return Ok(new MensagemSucessoDto(Resources.BUSCA_SUCESSO, Resources.STATUS_OK, medicos));
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
        public async Task<IActionResult> GetById([FromServices] IMedicoService _service, [FromQuery] Guid id)
        {
            try
            {
                MedicoDto medico = await _service.GetById(id);

                if (medico != null)
                {
					return Ok(new MensagemSucessoDto(Resources.BUSCA_SUCESSO, Resources.STATUS_OK, medico));
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
		public async Task<IActionResult> Delete([FromServices] IMedicoService _service, [FromQuery] Guid id)
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
