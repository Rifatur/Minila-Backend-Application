using System.ComponentModel.DataAnnotations;

namespace RidingApp.Presentation.DTOs
{
    public class RoadWayDTOs
    {
        [Key]
        public long Id { get; set; }
        public string RoadName { get; set; }
        public string RoadCode { get; set; }
        public long schoolId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
