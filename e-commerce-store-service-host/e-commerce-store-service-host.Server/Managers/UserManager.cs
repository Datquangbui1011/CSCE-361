using e_commerce_store_service_host.Server.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_commerce_store_service_host.Server.Accessors;
using e_commerce_store_service_host.Server.Model.Entities;
using Microsoft.EntityFrameworkCore;



namespace e_commerce_store_service_host.Server.Services;
public class UserManager
{
    private readonly UserAccessor _userRepository;

    public UserManager(UserAccessor userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user;
    }

  

    public async Task AddUserAsync(User user)
    {
        await _userRepository.AddAsync(user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user != null)
        {
            _userRepository.Delete(user);
        }
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetbyEmailAsync(email);
        return user;
    }

}