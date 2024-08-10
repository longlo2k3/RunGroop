using Object.Data.Enum;
using Object.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Object.ViewModel
{
    public class EditRaceViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? URL {  get; set; }
        public int AddressID { get; set; }
        public Address Address { get; set; }

        public RaceCategory RaceCategory { get; set; }
    }
}
