using Aerariu.Core.Models;

namespace Aerariu.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> CheckPasswordAsync(User user, string password);
        Task CreateUserAsync(User user, string password);
        Task<IEnumerable<string>> GetRolesAsync(User user);
    }
}
