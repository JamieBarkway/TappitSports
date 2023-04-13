using System.ComponentModel.DataAnnotations;

namespace TappitSports.Data
{
    public class Sport
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool isEnabled { get; set; }
    }
}
