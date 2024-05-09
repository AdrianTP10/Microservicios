using OrderMicroservice.Core.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMicroservice.Core.OrdersDTO
{
    public class OrderWithUser
    {
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        [StringLength(12)]
        public string Status { get; set; }
        [Required]
        public int IdProduct { get; set; }
        [Required]
        public UserDTO UserDTO { get; set; }
    }
}
