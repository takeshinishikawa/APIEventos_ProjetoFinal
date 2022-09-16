using System.ComponentModel.DataAnnotations;

namespace APIEventos.Core.Models
{
    public class CityEvent
    {
        [Key]
        public long IdEvent { get; set; }
        [MinLength(0, ErrorMessage = "Não existe Nome do Evento vazio.")]
        [Required(ErrorMessage = "É necessário informar o Nome do Evento.")]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Data é obrigatória.")]
        public DateTime DateHourEvent { get; set; }
        [MinLength(0, ErrorMessage = "Não existe Local vazio.")]
        [Required(ErrorMessage = "É necessário informar um Local.")]
        public string Local { get; set; }
        public string? Address { get; set; }
        public decimal Price { get; set; }
        [Required(ErrorMessage = "É necessário informar o Status do Evento.")]
        public bool Status { get; set; }
    }
}
