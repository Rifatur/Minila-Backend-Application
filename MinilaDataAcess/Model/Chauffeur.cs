using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinilaDataAcess.Model
{
    public class Chauffeur
    {
        [Key]
        public long id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }

        public string? CRCode { get; set; }
        public Boolean Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }
        //@Html.DisplayFor(m => m.StartDate) for View
    }
}
