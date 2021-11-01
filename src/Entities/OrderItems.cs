using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class OrderItems
    {
        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        public virtual Orders Order { get; set; }

        // public int Quantity { get; set; }

        public int TicketID { get; set; }
        [ForeignKey("TicketID")]
        public virtual Tickets Ticket { get; set; }
    }
}