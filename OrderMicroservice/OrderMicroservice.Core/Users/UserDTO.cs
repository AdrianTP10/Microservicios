using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMicroservice.Core.Users
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(24)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(26)]
        public string LastName { get; set; }
        [Required]
        [StringLength(12)]
        public string Role { get; set; }
        [Required]
        [BindProperty, DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }
        [Required]
        public int IdCity { get; set; }
    }
}
