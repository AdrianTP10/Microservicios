using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.Client.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }
        [Required, StringLength(12)] public string Role { get; set; }
        [Required]
        [Range(1, 100)]
        public int CityId { get; set; }
        [Required]
        [BindProperty, DisplayFormat(DataFormatString = "{0:dd-MM-yyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }
    }
}
