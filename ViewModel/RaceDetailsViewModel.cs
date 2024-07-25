using Object.Models;

namespace Object.ViewModel
{
    public class RaceDetailsViewModel
    {
        public Races races { get; set; }
        public IEnumerable<Races> raceList { get; set; }
    }
}
