using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinilaDataAcess.Model
{
    public class AppUsers : IdentityUser
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [MaxLength(50)]
        public string? NationalID { get; set; }
        public string? UserCode { get; set; }
        public string? loginIdentity { get; set; }
        public Boolean Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }
        //@Html.DisplayFor(m => m.StartDate) for View
    }
}
