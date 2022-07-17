using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinilaDataAcess.Model
{
    public class RoadWay
    {
        [Key]
        public long id { get; set; }
        public string? RoadName { get; set; }
        public string? RoadCode { get; set; }
        public string? schoolId { get; set; }
    }
}
