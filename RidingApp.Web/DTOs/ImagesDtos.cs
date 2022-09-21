using System.ComponentModel.DataAnnotations.Schema;

namespace RidingApp.Web.DTOs
{
    public class ImagesDtos
    {
        public long Id { get; set; }
        public string ProfilePicture { get; set; } = string.Empty;
        public string CarPicture { get; set; } = string.Empty;
        public string identityID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}
