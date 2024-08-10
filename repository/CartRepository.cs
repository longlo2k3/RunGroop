using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Object.Data;
using Object.Interfaces;
using Object.Models;

namespace Object.repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Cart cart)
        {
            _context.Add(cart);
            return Save();
        }

        public bool Delete(Cart cart)
        {
            _context.Remove(cart);
            return Save();
        }

        public  async Task<IEnumerable<Cart>> GetAll()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task<Cart> GetByIdAsync(int id)
        {
            return await _context.Carts.FirstOrDefaultAsync(p => p.Id == id);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Cart cart)
        {
            _context.Update(cart);
            return Save();
        }
    }
}
