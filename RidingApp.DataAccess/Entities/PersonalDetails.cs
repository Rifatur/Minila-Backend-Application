using RidingApp.DataAccess.Entities.Common;

namespace RidingApp.DataAccess.Entities
{
    public class PersonalDetails : BaseEntity
    {
        public string UserID { get; set; }
        public string Address { get; set; }
        public long SchoolId { get; set; }
        public long NID { get; set; }
        public string IdentityCard { get; set; }
        public string licenseNo { get; set; }

    }
}
