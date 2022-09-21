using System.ComponentModel.DataAnnotations;

namespace RidingApp.DataAccess.Entities.Common
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
