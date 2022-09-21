using RidingApp.DataAccess.Entities.Common;

namespace RidingApp.DataAccess.Entities
{
    public class SystemImage : BaseEntity
    {
        public string ProfilePicture { get; set; } = string.Empty;
        public string CarPicture { get; set; } = string.Empty;
        public string identityID { get; set; }


    }
}
