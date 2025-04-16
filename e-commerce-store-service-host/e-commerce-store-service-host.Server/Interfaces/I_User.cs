using e_commerce_store_service_host.Server.Model.Entities;

namespace e_commerce_store_service_host.Server.Respository;

public interface I_User
{
    Task<List<User>> GetAllAsynce();
    Task<User?> GetByIdAsync(int userId);
    Task CreateAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);

}
