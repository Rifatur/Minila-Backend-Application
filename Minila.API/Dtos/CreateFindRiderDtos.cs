using System.ComponentModel.DataAnnotations;

namespace Minila.API.Dtos
{
    public class CreateFindRiderDtos
    {
        public long id { get; set; }
        public int ChauffeurId { get; set; }
        public int RoadId { get; set; }
        public int SchholId { get; set; }
        public string? remark { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime lastUpdateTime { get; set; }
    }
}
