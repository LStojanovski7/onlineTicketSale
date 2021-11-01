using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfOrder { get; set; }
        public decimal Total { get; set; }
    }
}