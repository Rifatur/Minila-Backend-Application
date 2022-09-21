using System.ComponentModel.DataAnnotations;

namespace RidingApp.Presentation.DTOs
{
    public class CreateSchoolDTOs
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
