using RidingApp.DataAccess.Entities.Common;

namespace RidingApp.DataAccess.Entities
{
    public class Vehicle : BaseEntity
    {
        public string VehicleName { get; set; }
        public string VehicleNumber { get; set; }
        public string SeatCapacity { get; set; }
    }
}
