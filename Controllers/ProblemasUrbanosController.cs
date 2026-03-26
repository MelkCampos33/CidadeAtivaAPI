using CidadeAtivaApi.DTOs;
using CidadeAtivaApi.Models.Enum;
using CidadeAtivaApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace CidadeAtivaApi.Controllers
{
    public class ProblemasUrbanosController : ControllerBase
    {   
        private readonly ProblemasService _service; // readonly defini o valor uma única vez 
        public ProblemasUrbanosController(ProblemasService service) => _service = service;


        // GET /api/problemas-urbanos/{id}
        [HttpGet ("{id:guid}")]

        public async Task<IActionResult> GetbyId(Guid id)
        {
            var problema = await _service.GetByIdAsync(id);
                return NotFound(new { mensagem = $"O {id} não foi encontrado. Tente Novamente"});
            return Ok(problema); // deu bom
        }
    }
}