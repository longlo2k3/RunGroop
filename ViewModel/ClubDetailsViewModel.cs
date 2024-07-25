using Object.Models;

namespace Object.ViewModel
{
    public class ClubDetailsViewModel
    {
        public Club Club { get; set; }
        public IEnumerable<Club> ClubList { get; set; }
    }
}
