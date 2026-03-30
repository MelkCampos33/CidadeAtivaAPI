using CidadeAtivaApi.Data;
using CidadeAtivaApi.DTOs;
using CidadeAtivaApi.Models.Enum;
using CidadeAtivaApi.Models;
using Microsoft.EntityFrameworkCore;


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

            if (tipo.HasValue) // hasValue verifica se  a variavel tem valor ou é null
                query = query.Where(problema => problema.Tipo == tipo.Value);

            if (status.HasValue)
                query =  query.Where(problema => problema.Status == status.Value);

            // Busca no banco de dados
            // 1. Ordena os dados pela data
            // 2. Executa a query no banco
            // 3. Mostra os dados como uma lista
            var lista = await query 
                .OrderByDescending(problema => problema.CriadoEm)
                .ToListAsync();

            // Pecorre a lista (select) => converte cada item com "ToDto
            // e depois retorna uma lista nova
            return lista.Select(problema => ToDto(problema)).ToList();
        }

            // --- GET BY ID ---
            public async Task<RespostaProblemaDTO?> GetByIdAsync(Guid id) // guid para fazer um "unique id"
        {
            var problema = await _db.Problamas.FindAsync(id);
            return problema is null ? null : ToDto(problema);
        }

            // --- CREATE ---
            public async Task<RespostaProblemaDTO> CreateAsync(CriarProblema dto)
        {
            var problemaTask = new ProblamasUrbano
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Tipo = dto.Tipo,
                Bairro = dto.Bairro
            };

            _db.Problamas.Add(problemaTask);
            await _db.SaveChangesAsync();
            return ToDto(problemaTask);
        }

        // --- UPDATE ---
        public async Task<RespostaProblemaDTO?> UpdateAsync(Guid id, AtualizarProblema dto) 
        {
            // Linha do Entity Framework
            // em SQl seria como 
            // SELECT * FROM Problemas WHERE Id = 'seu-guid'
            var problema = await _db.Problamas.FindAsync(id);
            if(problema is null) return null; // validação de campo vazio

            problema.Titulo = dto.Titulo;
            problema.Descricao = dto.Descricao;
            problema.Tipo = dto.Tipo;
            problema.Status = dto.Status;
            problema.Bairro =  dto.Bairro;
            problema.AtualizadoEm = DateTime.UtcNow;

            await _db.SaveChangesAsync(); // salvando as alteraçoes no banco de dados
            return ToDto(problema);
        }


        // --- DELETE ---
        public async Task<bool> DeleteAsync(Guid id)
        {
            var problema = await _db.Problamas.FindAsync(id);
            if (problema is null) return false; // se caso nao achar o ID retorna false'
            

            _db.Problamas.Remove(problema);
            await _db.SaveChangesAsync(); // se achar ele executa o 'DELETE'
            return true;
        }


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

