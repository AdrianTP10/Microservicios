using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PaymentMicroservice.Client.Models
{
    public class PaymentViewModel
    {
        public int Id { get; set; }
        [Required] public int OrderId { get; set; }
        [Required]
        [Column(TypeName = "decimal(13,2)")]
        public double Amount { get; set; }
    }
}
