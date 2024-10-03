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
    public class PacienteController : BaseController
    {
        public IVerificacoes _verificacoes;

        public PacienteController(ILogger<BaseController> logger, IVerificacoes verificacoes) : base(logger)
        {
            _verificacoes = verificacoes;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PacienteDto paciente, [FromServices] IPacienteService _service)
        {
            try
            {
				PacienteDto pacienteAdd = await _service.Add(paciente);

                return Ok(new MensagemSucessoDto(Resources.INCLUIDO_SUCESSO, Resources.STATUS_OK));

            }
            catch (Exception ex)
            {
                return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message, Resources.STATUS_BAD_REQUEST));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PacienteDto paciente, [FromServices] IPacienteService _service)
        {
            try
            {
                PacienteDto pacienteAdd = await _service.Update(paciente);

                return Ok(new MensagemSucessoDto(Resources.ATUALIZADO_SUCESSO, Resources.STATUS_OK));
   

            }
            catch (Exception ex)
            {
                return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message, Resources.STATUS_BAD_REQUEST));
            }
        }

		[HttpGet]
		public async Task<IActionResult> GetAll([FromServices] IPacienteService _service)
		{
			try
			{
				IEnumerable<PacienteDto> pacientes = await _service.GetAll();

				if (pacientes != null)
				{
					return Ok(new MensagemSucessoDto(Resources.BUSCA_SUCESSO, Resources.STATUS_OK, pacientes));
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
        public async Task<IActionResult> GetById([FromServices] IPacienteService _service, [FromQuery] Guid id)
        {
            try
            {
                PacienteDto paciente = await _service.GetById(id);

                if (paciente != null)
                {
					return Ok(new MensagemSucessoDto(Resources.BUSCA_SUCESSO, Resources.STATUS_OK, paciente));
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
		public async Task<IActionResult> Delete([FromServices] IPacienteService _service, [FromQuery] Guid id)
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
