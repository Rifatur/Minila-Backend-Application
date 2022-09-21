using System.ComponentModel.DataAnnotations;

namespace RidingApp.Web.DTOs.ViewModels
{
    public class SchoolWebDTOs
    {
        [Key]
        public long Id { get; set; }
        public string SchoolName { get; set; }
        public Boolean status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
