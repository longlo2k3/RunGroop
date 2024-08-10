using Microsoft.EntityFrameworkCore;
using Object.Data;
using Object.Interfaces;
using Object.Models;

namespace Object.repository
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext _context;
        public RaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Races race)
        {
            _context.Add(race);
            return Save();
        }

        public bool Delete(Races race)
        {
            _context.Remove(race);
            return Save();
        }

        public async Task<IEnumerable<Races>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<Races> GetByIdAsync(int id)
        {
            return await _context.Races.Include(a=>a.Address).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Races> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Races.Include(a => a.Address).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Races>> GetRaceByCity(string city)
        {
            return await _context.Races.Include(a=>a.Address).Where(c=>c.Address.City.Contains(city)).ToListAsync(); 
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >0 ? true : false;
        }

        public bool Update(Races race)
        {
            _context.Update(race);
            return Save();
        }
    }
}
