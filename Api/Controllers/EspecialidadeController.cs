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
    public class EspecialidadeController : BaseController
    {
        public IVerificacoes _verificacoes;

        public EspecialidadeController(ILogger<BaseController> logger, IVerificacoes verificacoes) : base(logger)
        {
            _verificacoes = verificacoes;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EspecialidadeDto especialidade, [FromServices] IEspecialidadeService _service)
        {
            try
            {
				EspecialidadeDto especialidadeAdd = await _service.Add(especialidade);

                return Ok(new MensagemSucessoDto(Resources.INCLUIDO_SUCESSO, Resources.STATUS_OK));

            }
            catch (Exception ex)
            {
                return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message, Resources.STATUS_BAD_REQUEST));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EspecialidadeDto especialidade, [FromServices] IEspecialidadeService _service)
        {
            try
            {
                EspecialidadeDto especialidadeAdd = await _service.Update(especialidade);

                return Ok(new MensagemSucessoDto(Resources.ATUALIZADO_SUCESSO, Resources.STATUS_OK));
   

            }
            catch (Exception ex)
            {
                return BadRequest(new MensagemErroDto(Resources.ERRO_EXEC_METODO + ex.Message, Resources.STATUS_BAD_REQUEST));
            }
        }

		[HttpGet]
		public async Task<IActionResult> GetAll([FromServices] IEspecialidadeService _service)
		{
			try
			{
				IEnumerable<EspecialidadeDto> especialidades = await _service.GetAll();

				if (especialidades != null)
				{
					return Ok(new MensagemSucessoDto(Resources.BUSCA_SUCESSO, Resources.STATUS_OK, especialidades));
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
        public async Task<IActionResult> GetById([FromServices] IEspecialidadeService _service, [FromQuery] Guid id)
        {
            try
            {
                EspecialidadeDto especialidade = await _service.GetById(id);

                if (especialidade != null)
                {
					return Ok(new MensagemSucessoDto(Resources.BUSCA_SUCESSO, Resources.STATUS_OK, especialidade));
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
		public async Task<IActionResult> Delete([FromServices] IEspecialidadeService _service, [FromQuery] Guid id)
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
