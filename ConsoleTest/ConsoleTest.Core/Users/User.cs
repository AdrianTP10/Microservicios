using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Core.Users
{
    public class User
    {
        [Key] public int Id { get; set; }

        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }
        [Required, StringLength(12)] public string Role { get; set; }
        [Required] public int CityId { get; set; }
        public City City { get; set; }  
        [Required] 
        public DateTime Birthday { get; set; }
    }
}
