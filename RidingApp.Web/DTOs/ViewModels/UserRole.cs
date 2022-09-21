using System.ComponentModel.DataAnnotations.Schema;

namespace RidingApp.Web.DTOs.ViewModels
{
    public class UserRole
    {
        [NotMapped]
        public int Id { get; set; }
        [NotMapped]
        public string Name { get; set; }

    }
}
