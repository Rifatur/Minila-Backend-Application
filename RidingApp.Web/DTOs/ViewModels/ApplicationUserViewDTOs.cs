using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace RidingApp.Web.DTOs.ViewModels
{
    public class ApplicationUserViewDTOs : IdentityUser
    {
        public string Name { get; set; }
        [NotMapped]
        public string RoleId { get; set; }
        [NotMapped]
        public string Role { get; set; }

    }
}
