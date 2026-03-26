using CidadeAtivaApi.DTOs;
using CidadeAtivaApi.Models;
using CidadeAtivaApi.Services;

namespace CidadeAtivaApi.Extensions
{
    public class ProblemasUbanosExtensions
    {
         // Onde é convertido a entidade em DTO de resposta
        private static RespostaProblemaDTO ToDto(ProblamasUrbano p) => new()
        {
            Id = p.Id,
            Titulo = p.Titulo,
            Descricao = p.Descricao,
            Tipo = p.Tipo.ToString(),
            Status = p.Status.ToString(),
            Bairro = p.Bairro,
            CriadoEm = p.CriadoEm,
            AtualizadoEm = p.AtualizadoEm
        };

    }
}