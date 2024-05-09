using OrderMicroservice.Core.Products;
using OrderMicroservice.Core.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMicroservice.Core.OrdersDTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        [StringLength(12)]
        public string StatusOrder { get; set; }
        [Required]
        public DateOnly ArriveOrder { get; set; }
        [Required]
        public Product Product { get; set; }
        [Required]
        public User User { get; set; }
    }
}
