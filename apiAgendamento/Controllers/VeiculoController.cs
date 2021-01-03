using System.Collections.Generic;
using System.Threading.Tasks;
using apiAgendamento.Dal;
using apiAgendamento.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiAgendamento.Controllers
{
    [ApiController]
    [Route("v1/Veiculo")]
    public class VeiculoController : ControllerBase
    {
        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult>
        ListarPorData(
            [FromServices] IVeiculoDal veiculoDal
        )
        {
            try
            {
                List<Veiculo> retorno =
                    await veiculoDal.FindAll();
                if (retorno != null)
                {
                    return Ok(retorno);
                }
                else
                {
                    return NotFound(new {
                        message =
                            "Nenhum veiculo encontrado."
                    });
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}