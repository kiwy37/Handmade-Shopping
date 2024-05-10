using Daw.DataLayer.DataBaseConenction;
using Daw.DataLayer.DTO;
using Daw.DataLayer.Models;
using Daw.DataLayer.Repositories;

namespace Daw.DataLayer.Services
{
    public class UserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly AppDbContext _appContext;
        public UserService(AppDbContext appContext, UnitOfWork unitOfWork)
        {
            _appContext = appContext;
            _unitOfWork = unitOfWork;
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _unitOfWork.UserRepository.GetByIdAsync(id);
        }
        public async Task<User?> GetUserByNameAsync(string name)
        {
            return await _unitOfWork.UserRepository.GetUserByNameAsync(name);
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.UserRepository.GetAllAsync();
        }
        public async Task AddUserAsync(UserRegistrationDTO user)
        {
            var newUser = new User
            {
                Name = user.Name,
                Password = user.Password
            };
            await _unitOfWork.UserRepository.AddAsync(newUser);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateUserAsync(User user)
        {
            _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteUserAsync(int id)
        {
            _unitOfWork.UserRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<User?> Login(UserRegistrationDTO user)
        {
            return await _unitOfWork.UserRepository.Login(user);
        }
    }
}
