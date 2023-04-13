using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TappitSports.Data
{
    [Table("sports", Schema = "tappittechnicaltest")]
    public class Sport
    {
        [Required]
        public int sportid { get; set; }
        public string name { get; set; }
        public bool isenabled { get; set; }
    }
}
