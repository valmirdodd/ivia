using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using apiAgendamento.Dal;
using apiAgendamento.Models;

namespace apiAgendamento.Controllers
{
    [ApiController]
    [Route("v1/Agendamento")]
    public class AgendamentoController : ControllerBase
    {
        [HttpGet]
        [Route("DataAgendamento/{dtAgendamento}")]
        public async Task<IActionResult>
        ListarPorData(
            [FromServices] IAgendamentoDal agendamentoDal,
            DateTime dtAgendamento
        )
        {
            try
            {
                List<Agendamento> retorno =
                    await agendamentoDal.FindAllByDate(dtAgendamento);
                if (retorno != null)
                {
                    return Ok(retorno);
                }
                else
                {
                    return NotFound(new {
                        message =
                            "Nenhum agendamento encontrado na data informada."
                    });
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult>
        InserirAgenda(
            [FromServices] IAgendamentoDal agendamentoDal,
            [FromBody] Agendamento body
        )
        {
            try
            {
                Agendamento retorno = await agendamentoDal.Insert(body);
                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
