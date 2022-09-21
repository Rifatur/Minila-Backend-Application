using System.ComponentModel.DataAnnotations;

namespace RidingApp.Web.DTOs.ViewModels
{
    public class TripRequestWebDTOs
    {
        public long id { get; set; }
        public string StudetID { get; set; }
        public string ChauffeurId { get; set; }
        public string lat { get; set; }
        public string lag { get; set; }
        public string location { get; set; }
        public string road { get; set; }
        public int status { get; set; }
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }
        [DataType(DataType.Time)]
        public DateTime RequestTime { get; set; }
    }
}
