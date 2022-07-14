using System.ComponentModel.DataAnnotations;

namespace Minila.API.Dtos
{
    public class UpdateAPIDtos
    {
        public long id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? NationalID { get; set; }
        public string? UserCode { get; set; }
        public Boolean Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }
    }
}
