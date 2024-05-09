using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Core.Payments
{
    public class Payment
    {
        [Key] public int Id { get; set; }
        [Required] public int OrderId { get; set; }
        [Required]
        [Column(TypeName = "decimal(13,2)")]
        public double Amount { get; set; }
    }
}
