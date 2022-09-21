namespace RidingApp.Web.DTOs.ViewModels
{
    public class PersonalDetailsWebDTOs
    {
        public long id { get; set; }
        public string UserID { get; set; }
        public string Address { get; set; }
        public long SchoolId { get; set; }
        public long NID { get; set; }
        public string IdentityCard { get; set; }
        public string licenseNo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
