using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TappitSports.Data
{
    [Table("people", Schema = "tappittechnicaltest")]
    public class Person
    {
        [Required]
        public int personid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public bool isauthorised { get; set; }
        public bool isvalid { get; set; }
        public bool isenabled { get; set; }
    }
}
