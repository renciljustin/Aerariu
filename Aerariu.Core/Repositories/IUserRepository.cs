using Aerariu.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerariu.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> CheckPasswordAsync(User user, string password);
        Task CreateUserAsync(User user, string password);
        Task<IEnumerable<string>> GetRolesAsync(User user);
    }
}
