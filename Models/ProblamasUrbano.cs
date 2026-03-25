using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CidadeAtivaApi.Models.Enum;

namespace CidadeAtivaApi.Models
{
    public class ProblamasUrbano
    {
        // GUID: é um Unique identifer pro ID, parecido da forma que funciona no SQL
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Titulo { get; set; } // "?" Para essa variavel nunca ser null
        public string? Descricao { get; set; }

        // Importanto os Enum
        public TipoProblema Tipo { get; set; }
        public StatusProblema Status { get; set; } = StatusProblema.Aberto;
        
        public string? Bairro { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow; // Formatação da data em UTC
        public DateTime? AtualizadoEm { get; set; }
    }
}

