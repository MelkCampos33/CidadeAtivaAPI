using CidadeAtivaApi.Data;
using CidadeAtivaApi.DTOs;
using CidadeAtivaApi.Models.Enum;
using CidadeAtivaApi.Models;
using CidadeAtivaApi.Models.Enum;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using CidadeAtivaApi.Extensions;



namespace CidadeAtivaApi.Services
{
    public class ProblemasService
    {
        // Todas logica do sistema
        //
        // O service recebe o AppDbContext via injecao de dependencia e so devolve DTOs 
        // A injeção de dependencia seria tipo ao inves de criar o AppDB manualmente
        // voce recebe ele pronto pelo construtor (processo feito pelo ASP.NET)

        private readonly AppDB _db; // readonly: só pode ser definido uma vez
        public ProblemasService (AppDB db) => _db = db; // construtor simplificado

        // o ASP.NET cria o AppDB => Injeta no construtor => Salva em _db

        // --- GET ALL ---
        public async Task<List<RespostaProblemaDTO>> GetAllAsync(TipoProblema? tipo, StatusProblema? status)
        {   
            // Consulta que pode ser modificada
            // permite montar consultas que ainda não foram executadas
            // Serve pra ir montando a consulta aos poucos, fazendo varios tipos de filtros diferentes
            var query = _db.Problamas.AsQueryable(); 

            if (tipo.HasValue) // hasValue verifica se  a avriavel tem valor ou é null
                query = query.Where(problema => problema.Tipo == tipo.Value);

            if (status.HasValue)
                query =  query.Where(problema => problema.Status == status.Value);

            return await query
                .OrderByDescending(problema => problema.CriadoEm)
                .Select(problema => problema.ToDto())
                .ToListAsync(); 
        }

            // --- GET BY ID ---
            public async Task<RespostaProblemaDTO?> GetByIdAsync(Guid id) // guid para fazer um "unique id"
        {
            var problema = await _db.Problamas.FindAsync(id);
            return problema is null ? null : ToDto(problema);
        }

            // --- CREATE ---

    }
}

