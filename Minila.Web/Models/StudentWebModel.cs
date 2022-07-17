using System.ComponentModel.DataAnnotations;

namespace Minila.Web.Models
{
    public class StudentWebModel
    {
        public long id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [MaxLength(50)]
        public string? SchoolId { get; set; }
        public string? UserCode { get; set; }
        public Boolean Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }
    }
}
