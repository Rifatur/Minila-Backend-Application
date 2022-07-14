using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinilaDataAcess.Dtos
{
    public class UserRegisterDaDtos
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }

        public string? UserCode { get; set; }
        [Required]
        public string? loginIdentity { get; set; }
        public Boolean Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
    }
}
