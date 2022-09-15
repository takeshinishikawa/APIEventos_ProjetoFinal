using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIEventos.Core.Models
{
    public class EventReservation
    {
        //[Key]
        public long IdReservation { get; set; }
        [Required(ErrorMessage = "É necessário informar o Id de um evento válido.")]
        public long IdEvent { get; set; }
        [MinLength(0, ErrorMessage = "Não existe Nome de Cliente vazio.")]
        [Required(ErrorMessage = "É necessário informar o Nome do Titular da reserva.")]
        public string PersonName { get; set; }
        [Required(ErrorMessage = "É necessário informar a quantidade de pessoas da reserva.")]
        [Range(1, long.MaxValue, ErrorMessage = "A quantidade de pessoas deve ser maior que 0")]
        public long Quantity { get; set; }
    }
}
