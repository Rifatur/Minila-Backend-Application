using System.ComponentModel.DataAnnotations;

namespace Minila.Web.Models
{
    public class TripRequestWebModel
    {
        public long id { get; set; }
        public string? StudetID { get; set; }
        public string? ChauffeurId { get; set; }
        public string? lat { get; set; }
        public string? lag { get; set; }
        public string? location { get; set; }
        public string? road { get; set; }
        public int status { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }
        [DataType(DataType.Time)]
        public DateTime RequestTime { get; set; }
    }
}
