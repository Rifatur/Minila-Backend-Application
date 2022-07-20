using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinilaDataAcess.Model
{
    public class FindRider
    {
        [Key]
        public long id { get; set; }
        public int ChauffeurId { get; set; }
        public int RoadId { get; set; }
        public int SchholId { get; set; }
        public string? remark { get; set; }
        public DateTime CreateDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }
        [DataType(DataType.Time)]
        public DateTime lastUpdateTime { get; set; }
        
    }
}
