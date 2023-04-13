using System.Security.Policy;

namespace TappitSports.Data
{
    public class PersonWithFavouriteSports
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isValid { get; set; }
        public bool isEnabled { get; set; }
        public bool isAuthorised { get; set; }
        public List<string> FavouriteSports { get; set; }
    }
}
