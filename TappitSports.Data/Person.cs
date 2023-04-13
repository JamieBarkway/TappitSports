using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TappitSports.Data
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        public string firstName { get; set; }
        public string lastName { get; set; }
        public bool isAuthorised { get; set; }
        public bool isValid { get; set; }
        public bool isEnabled { get; set; }
    }
}
