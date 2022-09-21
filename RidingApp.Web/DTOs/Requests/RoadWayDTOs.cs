namespace RidingApp.Web.DTOs.Requests
{
    public class RoadWayDTOs
    {
        public long Id { get; set; }
        public string RoadName { get; set; }
        public string RoadCode { get; set; }
        public long schoolId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
