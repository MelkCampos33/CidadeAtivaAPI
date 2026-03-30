using CidadeAtivaApi.DTOs;
using CidadeAtivaApi.Models.Enum;
using CidadeAtivaApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CidadeAtivaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class ProblemasUrbanosController : ControllerBase
    {   
        private readonly ProblemasService _service; // readonly defini o valor uma única vez 
        public ProblemasUrbanosController(ProblemasService service) => _service = service;


        // GET /api/problemas-urbanos/{id}
        [HttpGet]
        public async Task<IActionResult> GetAll( // Task<IActionResult>: - retorna uma resposta HTTP
            [FromQuery] TipoProblema? tipo, 
            [FromQuery] StatusProblema? status) 
        {
            var lista = await _service.GetAllAsync(tipo, status);
            return Ok(lista);     
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var problema = await _service.GetByIdAsync(id);
            if(problema is null)
            {
                return NotFound(new { mensagem = $"ID {id} não encontrado" });
            }
            return Ok(problema);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CriarProblema dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); 


            var criado = await _service.CreateAsync(dto);
            return CreatedAtAction(
                nameof(GetById),
                new { id = criado.Id },
                criado); 
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] AtualizarProblema dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);  

            var atualizado = await _service.UpdateAsync(id, dto);
            if (atualizado is null)
                return NotFound(new { mensagem = $"Problema {id} nao encontrado" });
            return Ok(atualizado); 
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var removido = await _service.DeleteAsync(id);
            if (!removido)
                return NotFound(new { mensagem = $"Problema {id} nao encontrado" });
            return NoContent(); 
        }

        
    }
}