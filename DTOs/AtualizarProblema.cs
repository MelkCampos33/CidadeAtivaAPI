using System.ComponentModel.DataAnnotations;
using CidadeAtivaApi.Models.Enum;

namespace CidadeAtivaApi.DTOs
{
    public class AtualizarProblema
    {
        [Required] [MinLength(5)] [MaxLength(20)]
        public string? Titulo {get; set;}

        [MaxLength(140)]
        public string? Descricao {get; set;}

        [Required]
        [EnumDataType(typeof(TipoProblema), ErrorMessage = " Esse tipo é invalido ")] // "EnumType" valida se o valor usado pertence aquele enum 
        public TipoProblema Tipo {get; set;}

        [Required]
        [EnumDataType(typeof(StatusProblema), ErrorMessage= " Esse status é invalido ")] 
        public StatusProblema Status {get; set;}

        [Required][MaxLength(100)]
        public string? Bairro {get; set;}

    }
}