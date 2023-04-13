using System.ComponentModel.DataAnnotations.Schema;

namespace TappitSports.Models
{
    [Table("favouritesports", Schema = "tappittechnicaltest")]
    public class FavouriteSport
    {
        public int personid { get; set; }
        public int sportid { get; set; }
    }
}
