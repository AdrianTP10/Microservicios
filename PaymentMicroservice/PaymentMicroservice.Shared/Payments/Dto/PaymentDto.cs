using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentMicroservice.Payments.Dto
{
    public class PaymentDto
    {
        public int Id { get; set; }
        [Required] public int OrderId { get; set; }
        [Required]
        [Column(TypeName = "decimal(13,2)")]
        public double Amount { get; set; }
    }
}
