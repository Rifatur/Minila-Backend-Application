using RidingApp.DataAccess.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace RidingApp.DataAccess.Entities
{
    public class Trip : BaseEntity
    {
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

    }
}
