using System.ComponentModel.DataAnnotations;

namespace RidingApp.Presentation.DTOs
{
    public class VehiclePreDTOs
    {
        [Key]
        public long Id { get; set; }
        public string VehicleName { get; set; }
        public string VehicleNumber { get; set; }
        public string SeatCapacity { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
