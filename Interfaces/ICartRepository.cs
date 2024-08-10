using Microsoft.AspNetCore.Mvc;
using Object.Models;

namespace Object.Interfaces
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetAll();
        Task<Cart> GetByIdAsync(int id);
        bool Add(Cart cart);
        bool Update(Cart cart);
        bool Delete(Cart cart);
        bool Save();
    }
}
