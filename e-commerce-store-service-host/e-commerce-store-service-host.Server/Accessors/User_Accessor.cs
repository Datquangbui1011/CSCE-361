using e_commerce_store_service_host.Server.Model;
using e_commerce_store_service_host.Server.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace e_commerce_store_service_host.Server.Accessors;
using e_commerce_store_service_host.Server.Model.Entities;

public interface IUserAccessor
{
        // Basic CRUD-like actions
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        void Delete(User user);
        Task SaveAsync();
   
}

public class User_Accessor : IUserAccessor
{ 
        private readonly AppDbContext _context;

        public User_Accessor(AppDbContext context)
        {
                _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
                return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
                return await _context.Users.FindAsync(id);
        }

        public async Task AddAsync(User user)
        {
                await _context.Users.AddAsync(user);
        }

        public void Delete(User user)
        {
                _context.Users.Remove(user);
        }

        public async Task SaveAsync()
        {
                await _context.SaveChangesAsync();
        }

}