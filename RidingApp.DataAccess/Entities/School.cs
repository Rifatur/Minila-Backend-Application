using RidingApp.DataAccess.Entities.Common;

namespace RidingApp.DataAccess.Entities
{
    public class School : BaseEntity
    {
        public string SchoolName { get; set; }
        public Boolean status { get; set; }
    }
}
