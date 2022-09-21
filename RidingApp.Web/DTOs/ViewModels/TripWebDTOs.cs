using System.ComponentModel.DataAnnotations;

namespace RidingApp.Web.DTOs.ViewModels
{
    public class TripWebDTOs
    {
        public long Id { get; set; }
        public string tripCode { get; set; }
        public string tripStatus { get; set; }
        [DataType(DataType.Time)]
        public DateTime tripStart { get; set; }
        [DataType(DataType.Time)]
        public DateTime tripEnd { get; set; }
        public string StudetID { get; set; }
        public string DriverId { get; set; }
        public string cancelReason { get; set; }
        public int startLocation { get; set; }
        public int endLocation { get; set; }
        public int DriverRating { get; set; }
        public int StudetRating { get; set; }
        public int TripRequestId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
