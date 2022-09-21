using System.ComponentModel.DataAnnotations;

namespace RidingApp.Presentation.DTOs
{
    public class RiderHubDTOs
    {
        [Key]
        public long Id { get; set; }
        public string RiderId { get; set; }
        public long RoadWayId { get; set; }
        public long SchholId { get; set; }
        public long CarId { get; set; }
        public string remark { get; set; }
        [DataType(DataType.Time)]
        public DateTime lastUpdateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
