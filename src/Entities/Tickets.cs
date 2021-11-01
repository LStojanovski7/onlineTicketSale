using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Tickets
    {
        [Key]
        public int TicketID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ValidTo { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}