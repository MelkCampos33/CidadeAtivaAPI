using CidadeAtivaApi.DTOs;
using CidadeAtivaApi.Models;

namespace CidadeAtivaApi.Extensions
{
    public static class ProblamasUrbanoExtensions
    {
        public static RespostaProblemaDTO ToDto(this ProblamasUrbano problema)
        {
            return new RespostaProblemaDTO
            {
                Id = problema.Id,
                Titulo = problema.Titulo,
                Descricao = problema.Descricao,
                Tipo = problema.Tipo.ToString(),
                Status = problema.Status.ToString(),
                Bairro = problema.Bairro,
                CriadoEm = problema.CriadoEm,
                AtualizadoEm = problema.AtualizadoEm
            };
        }
    }
}