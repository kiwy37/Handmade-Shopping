using Daw.DataLayer.DataBaseConenction;
using Daw.DataLayer.DTO;
using Daw.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Daw.DataLayer.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public async Task<User?> GetUserByNameAsync(string name)
        {
            if(_dbSet is null)
            {
                return null;
            }
            return await _dbSet.AsQueryable().FirstOrDefaultAsync(us => us.Name == name);

        }
        public async Task<User?> Login(UserRegistrationDTO user)
        {
            
            if(_dbSet is null)
            {
                return null;
            }
            return await _dbSet.AsQueryable().FirstOrDefaultAsync(us => us.Name == user.Name && us.Password == user.Password);
        }
    }
}
