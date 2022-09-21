using RidingApp.DataAccess.Entities.Common;

namespace RidingApp.DataAccess.Entities
{
    public class RoadWay : BaseEntity
    {
        public string RoadName { get; set; }
        public string RoadCode { get; set; }
        public long schoolId { get; set; }
    }
}
