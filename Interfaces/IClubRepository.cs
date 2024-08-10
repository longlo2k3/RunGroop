using Object.Models;

namespace Object.Interfaces
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAll();
        Task<Club> GetByIdAsync(int id);
        Task<IEnumerable<Club>>  GetClubByCity(string city);
        Task<Club?> GetByIdAsyncNoTracking(int id);
        bool Add(Club club);
        bool Update(Club club); 
        bool Delete(Club club);
        bool Save();

    }
}
