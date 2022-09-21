using RidingApp.DataAccess.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace RidingApp.DataAccess.Entities
{
    public class RiderHub : BaseEntity
    {
        public string RiderId { get; set; }
        public long RoadWayId { get; set; }
        public long SchholId { get; set; }
        public long CarId { get; set; }
        public string remark { get; set; }
        [DataType(DataType.Time)]
        public DateTime lastUpdateTime { get; set; }
    }
}
