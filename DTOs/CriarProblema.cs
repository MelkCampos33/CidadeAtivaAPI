using System.ComponentModel.DataAnnotations;
using CidadeAtivaApi.Models.Enum;


namespace CidadeAtivaApi.DTOs // DTOs (Data Transfer Objects) controlam o que entra e o que sai da API.
{
    public class CriarProblema
    {
        [Required(ErrorMessage = " O titulo é obrigatorio para criação da petição ")]
        [MinLength(5, ErrorMessage = " Minimo de 5 caracteres ")]
        [MaxLength(20, ErrorMessage = " Maximo de 20 caracteres ")]
        public string? Titulo { get; set; } 

        
        [MaxLength(140)] 
        public string? Descricao { get; set; }

        // Validação do tipo da requisição
        [Required(ErrorMessage = " Definir o tipo é obrigatorio ")]
        [EnumDataType(typeof(TipoProblema), ErrorMessage = " Tipo Invalido ")]
        public TipoProblema Tipo { get; set; }

        [Required(ErrorMessage = " Definir o bairro é obrigatorio ")]
        [MaxLength(100)] 
        public string? Bairro { get; set; }
    } 
}
